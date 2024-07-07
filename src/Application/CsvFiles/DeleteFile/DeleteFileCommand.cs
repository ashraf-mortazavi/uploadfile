using CsvFileUploadApp.Application.Operations;
using MediatR;

namespace CsvFileUploadApp.Application.CsvFiles.DeleteFile;

public record DeleteFileCommand() : IRequest<OperationResult>;