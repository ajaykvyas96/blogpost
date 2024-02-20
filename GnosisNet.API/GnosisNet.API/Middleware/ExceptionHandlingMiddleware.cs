using GnosisNet.Service.CustomException;
using GnosisNet.Service.Models;
using Npgsql;
using System.Net;
using System.Text.Json;

namespace GnosisNet.API.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        // private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            //_next = next;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            if (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            var errorResponse = new ErrorResponse
            {
                Success = false
            };
            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.Message = ex.Message;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = ex.Message;
                    break;
                case UserSpecificException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                case PostgresException ex:
                    switch (ex.SqlState)
                    {
                        case "23503":
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            errorResponse.Message = "Unable to perform the operation due to a foreign key constraint violation. Please make sure the referenced record exists.";
                            break;
                        default:
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            errorResponse.Message = ex.Message;
                            break;
                    }
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal Server errors. Check Logs!";
                    errorResponse.InnerException = exception.InnerException + " -- " + exception.Message;
                    break;
            }
            var result = JsonSerializer.Serialize(errorResponse);
            _logger.LogWarning($"******{result}**********");
            await context.Response.WriteAsync(result);
        }
    }
}
