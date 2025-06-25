
namespace ApplicationCore.DTOs;

public class OperationResult<T>
{
    public OperationType OperationType { get; }
    public OperationStatus Status { get; }
    public T? Entity { get; }
    public string? Message { get; }
    public bool IsSuccessful
    {
        get
        {
            return Status == OperationStatus.Succeeded;
        }
    }

    internal OperationResult(T? result, OperationType operationType, OperationStatus operationStatus, string? message)
    {
        Entity = result;
        OperationType = operationType;
        Status = operationStatus;
        Message = message;
    }
}

public class OperationResult
{
    public static OperationResult<T> Success<T>(T? entity, OperationType operationType, string? message)
    {
        return new OperationResult<T>(entity, operationType, OperationStatus.Succeeded, message);
    }

    public static OperationResult<T> Success<T>(T? entity, OperationType operationType)
    {
        return new OperationResult<T>(entity, operationType, OperationStatus.Succeeded, message: $"{operationType} operation for {typeof(T).Name} is successful.");
    }

    public static OperationResult<T> Failure<T>(OperationType operationType, string? message)
    {
        return new OperationResult<T>(default, operationType, OperationStatus.Failed, message);
    }

    public static OperationResult<T> Failure<T>(OperationType operationType)
    {
        return new OperationResult<T>(default, operationType, OperationStatus.Failed, $"{operationType} operation for {typeof(T).Name} failed.");
    }
}
