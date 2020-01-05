namespace InvitationGenerator.API.ErrorHandling
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        public ErrorHandlingMiddleware(RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            var errorCodeMapping = new Dictionary<Type, HttpStatusCode>
            {
                {typeof(NullReferenceException), HttpStatusCode.PreconditionFailed },
                {typeof(DirectoryNotFoundException), HttpStatusCode.NotFound },
                {typeof(FileNotFoundException), HttpStatusCode.NotFound },
                {typeof(ArgumentNullException), HttpStatusCode.PreconditionFailed },
                {typeof(InvalidDataException), HttpStatusCode.PreconditionFailed },
            };

            if (errorCodeMapping.ContainsKey(ex.GetType()))
            {
                code = errorCodeMapping[ex.GetType()];
            }

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
