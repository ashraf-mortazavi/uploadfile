using CsvFileUploadApp.Application.Operations;
using MediatR;

namespace CsvFileUploadApp.Application.CsvFiles.GetFiles;

public sealed record GetFilesQuery() : IRequest<OperationResult>;