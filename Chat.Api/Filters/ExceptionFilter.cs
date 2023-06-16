using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace Chat.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ExceptionFilter(ProblemDetailsFactory problemDetailsFactory)
        {
            _problemDetailsFactory = problemDetailsFactory;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                context.Result = new ObjectResult(_problemDetailsFactory.CreateProblemDetails(context.HttpContext, 400));
            }
            else if (context.Exception is NullReferenceException)
            {
                context.Result = new ObjectResult(_problemDetailsFactory.CreateProblemDetails(context.HttpContext, 404));
            }
            else
            {
                context.Result = new ObjectResult(_problemDetailsFactory.CreateProblemDetails(context.HttpContext, 500));
            }
        }
    }
}
