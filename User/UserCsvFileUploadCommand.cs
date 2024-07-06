using CsvFileUploadApp.Operations;
using MediatR;

namespace CsvFileUploadApp;

public sealed record UserCsvFileUploadCommand(IFormFile File) 
    : IRequest<OperationResult>;