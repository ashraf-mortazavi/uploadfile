namespace CsvFileUploadApp.Application.Operations;

public class OperationResult(OperationResultStatus status, object value)
{
    public readonly OperationResultStatus Status = status;
    public readonly object Value = value;
}

public enum OperationResultStatus
{
    Ok = 1,
    Created,
    InvalidRequest,
    NotFound,
    Unprocessable
}