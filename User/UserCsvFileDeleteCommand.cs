using CsvFileUploadApp.Operations;
using MediatR;

namespace CsvFileUploadApp;

public record UserCsvFileDeleteCommand() : IRequest<OperationResult>;