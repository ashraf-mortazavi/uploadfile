using CsvFileUploadApp.Application.Operations;
using MediatR;

namespace CsvFileUploadApp;

public record DeleteFileCommand() : IRequest<OperationResult>;