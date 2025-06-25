
using System.Security.Cryptography;

public enum EncryptionAlgo
{

    SHA256 = 0,
    SHA384 = 1,
    SHA512 = 2,
}

internal static class EncryptionAlgoExtensions
{
    internal static HashAlgorithmName GetHashAlgorithmName(this EncryptionAlgo algo)
    {
        HashAlgorithmName algoName = algo switch
        {
            EncryptionAlgo.SHA256 => HashAlgorithmName.SHA256,
            EncryptionAlgo.SHA384 => HashAlgorithmName.SHA384,
            EncryptionAlgo.SHA512 => HashAlgorithmName.SHA512,
            _ => throw new NotImplementedException(),
        };
        return algoName;
    }
}