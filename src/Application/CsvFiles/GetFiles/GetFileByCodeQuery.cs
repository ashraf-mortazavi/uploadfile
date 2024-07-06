using CsvFileUploadApp.Application.Operations;
using MediatR;

namespace CsvFileUploadApp.Application.CsvFiles.GetFiles;

public record GetFileByCodeQuery(long? Code) : IRequest<OperationResult>;