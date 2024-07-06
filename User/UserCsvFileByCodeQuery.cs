using CsvFileUploadApp.Operations;
using MediatR;

namespace CsvFileUploadApp;

public record UserCsvFileByCodeQuery(long? Code) : IRequest<OperationResult>;