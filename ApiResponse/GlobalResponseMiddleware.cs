using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AthenaUnionLibrary.ApiResponse
{
    public class GlobalResponseMiddleware(RequestDelegate next, ILogger<GlobalResponseMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred.");

                var response = new AthenaApiResponse<object>
                {
                    Success = false,
                    Message = "An unexpected error occurred. Please try again later.",
                    Errors = [ex.Message]
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }

}
