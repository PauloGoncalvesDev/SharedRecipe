using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedRecipe.Exceptions;
using SharedRecipe.Exceptions.ExceptionsBase;
using SharedRecipe.Reporting.Responses;
using System.Net;

namespace SharedRecipe.Api.Filters
{
    public class ExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception is SharedRecipeException)
                HandleSharedRecipeException(exceptionContext);
            else
                HandleDefaultException(exceptionContext);
        }

        private void HandleSharedRecipeException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception is ValidationException)
                HandleValidationException(exceptionContext);
            else if (exceptionContext.Exception is InvalidLoginException)
                HandleInvalidLoginException(exceptionContext);
        }

        private void HandleValidationException(ExceptionContext exceptionContext)
        {
            ValidationException validationsErrors = exceptionContext.Exception as ValidationException;

            exceptionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            exceptionContext.Result = new ObjectResult(new ErrorBaseResponseJson(validationsErrors.ErrorsMessage, false));
        }

        private void HandleDefaultException(ExceptionContext exceptionContext)
        {
            exceptionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            exceptionContext.Result = new ObjectResult(new ErrorBaseResponseJson(APIMSG.UNKNOW_ERROR, false));
        }

        private void HandleInvalidLoginException(ExceptionContext exceptionContext)
        {
            InvalidLoginException invalidLoginException = exceptionContext.Exception as InvalidLoginException;

            exceptionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            exceptionContext.Result = new ObjectResult(new ErrorBaseResponseJson(invalidLoginException.Message, false));
        }
    }
}
