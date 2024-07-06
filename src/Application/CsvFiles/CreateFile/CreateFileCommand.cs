using CsvFileUploadApp.Application.Operations;
using MediatR;

namespace CsvFileUploadApp.Application.CsvFiles.CreateFile;

public sealed record CreateFileCommand(IFormFile File) 
    : IRequest<OperationResult>;