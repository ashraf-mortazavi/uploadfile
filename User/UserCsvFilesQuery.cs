using CsvFileUploadApp.Operations;
using MediatR;

namespace CsvFileUploadApp;

public sealed record UserCsvFilesQuery() : IRequest<OperationResult>;