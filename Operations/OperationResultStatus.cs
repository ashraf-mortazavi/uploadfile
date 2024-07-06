namespace CsvFileUploadApp.Operations;

public enum OperationResultStatus
{
    Ok = 1,
    Created,
    InvalidRequest,
    NotFound,
    Unprocessable,
    Timeout
}