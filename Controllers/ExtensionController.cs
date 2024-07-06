using CsvFileUploadApp.Operations;
using Microsoft.AspNetCore.Mvc;

namespace CsvFileUploadApp.Controllers;

public static class ExtensionController
{
    public static ActionResult InternalReturnResponse(this ControllerBase controller, OperationResult operation)
    {
        object response = operation.Value;

        return operation.Status switch
        {
            OperationResultStatus.Ok => controller.Ok(response),
            OperationResultStatus.Created => controller.Created(string.Empty, response),
            OperationResultStatus.InvalidRequest => controller.BadRequest(response),
            OperationResultStatus.NotFound => controller.NotFound(response),
            OperationResultStatus.Unprocessable => controller.UnprocessableEntity(response),
            _ => controller.UnprocessableEntity(response)
        };
    }
}