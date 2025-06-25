
using System.Security.Cryptography;
using System.Text;

namespace ApplicationCore.Infrastructure;

public static class CryptoService
{
    private static readonly Random _random = new();
    private static readonly byte[] _aesKey = Encoding.UTF8.GetBytes("R7j92bPskqT3vL0uWmjFyK4X1ABeQDqC");

    public static string Encrypt(string toEncrypt, out string algoinfo)
    {
        //1. Pick random algorithm
        EncryptionAlgo encryptionAlgo = (EncryptionAlgo)_random.Next(Enum.GetValues(typeof(EncryptionAlgo)).Length);

        //2. Generate random iteration count and salt
        int iterations = _random.Next(10000, 50001);
        byte[] saltBytes = new byte[16];
        RandomNumberGenerator.Fill(saltBytes);

        //3. Derive bytes using Rfc2898DeriveBytes with selected hash
        HashAlgorithmName hashAlgorithm = encryptionAlgo.GetHashAlgorithmName();

        using Rfc2898DeriveBytes pkbdf2 = new Rfc2898DeriveBytes(toEncrypt, saltBytes, iterations, hashAlgorithm);
        byte[] deriveBytes = pkbdf2.GetBytes(32); // derive 256-bit key

        //4. Create final output
        string encryptedString = Convert.ToBase64String(deriveBytes);
        string saltString = Convert.ToBase64String(saltBytes);
        algoinfo = $"{(int)encryptionAlgo}|{iterations}|{saltString}";

        return encryptedString;
    }

    public static string EncryptWithAlgoInfo(string toEncrypt, string algoinfo)
    {
        // algoinfo format: "EncryptionAlgo(int)|Iterations(int)|Salt(base64)"
        if (string.IsNullOrWhiteSpace(algoinfo))
            throw new ArgumentException("algoinfo must not be null or empty", nameof(algoinfo));

        var parts = algoinfo.Split('|');
        if (parts.Length != 3)
            throw new FormatException("algoinfo must be in the format 'EncryptionAlgo|Iterations|SaltBase64'");

        // Parse values
        if (!Enum.TryParse(parts[0], out EncryptionAlgo encryptionAlgo))
            throw new FormatException("Invalid EncryptionAlgo value");

        if (!int.TryParse(parts[1], out int iterations))
            throw new FormatException("Invalid iteration count");

        byte[] saltBytes;
        try
        {
            saltBytes = Convert.FromBase64String(parts[2]);
        }
        catch
        {
            throw new FormatException("Invalid base64 salt string");
        }

        HashAlgorithmName hashAlgorithm = encryptionAlgo.GetHashAlgorithmName();

        // Derive key
        using var pbkdf2 = new Rfc2898DeriveBytes(toEncrypt, saltBytes, iterations, hashAlgorithm);
        byte[] derivedBytes = pbkdf2.GetBytes(32); // 256-bit

        return Convert.ToBase64String(derivedBytes);
    }

    public static string EncryptAES(string toEncrypt)
    {
        using Aes aes = Aes.Create();
        aes.Key = _aesKey;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        byte[] plainBytes = Encoding.UTF8.GetBytes(toEncrypt);
        byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        //Combine IV + ciphertext
        byte[] result = new byte[aes.IV.Length + cipherBytes.Length];
        Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
        Buffer.BlockCopy(cipherBytes, 0, result, 0, aes.IV.Length);

        return Convert.ToBase64String(result);
    }

    public static string DecryptAES(string base64CipherText)
    {
        byte[] fullCipher = Convert.FromBase64String(base64CipherText);

        using Aes aes = Aes.Create();
        aes.Key = _aesKey;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        //Extract IV + ciphertext
        byte[] iv = new byte[aes.BlockSize / 8]; //16 bytes for AES
        byte[] cipherBytes = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }
}
