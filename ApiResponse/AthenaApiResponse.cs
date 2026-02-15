using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace AthenaUnionLibrary.ApiResponse
{
    public class AthenaApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        public static AthenaApiResponse<T> Ok(T data, string? message = null)
            => new() { Success = true, Data = data, Message = message, StatusCode = StatusCodes.Status200OK };

        public static AthenaApiResponse<T> Created(T data, string? message = null)
            => new() { Success = true, Data = data, Message = message, StatusCode = StatusCodes.Status201Created };

        public static AthenaApiResponse<T> BadRequest(string message, List<string>? errors = null)
            => new() { Success = false, Message = message, Errors = errors, StatusCode = StatusCodes.Status400BadRequest };

        public static AthenaApiResponse<T> NotFound(string message)
            => new() { Success = false, Message = message, StatusCode = StatusCodes.Status404NotFound };

        public static AthenaApiResponse<T> Unauthorized(string message)
            => new() { Success = false, Message = message, StatusCode = StatusCodes.Status401Unauthorized };

        public static AthenaApiResponse<T> Forbidden(string message)
            => new() { Success = false, Message = message, StatusCode = StatusCodes.Status403Forbidden };

        public static AthenaApiResponse<T> UnprocessableEntity(string message, List<string>? errors = null)
            => new() { Success = false, Message = message, Errors = errors, StatusCode = StatusCodes.Status422UnprocessableEntity };
            
        public static AthenaApiResponse<T> Fail(string message, List<string>? errors = null)
            => new() { Success = false, Message = message, Errors = errors, StatusCode = StatusCodes.Status500InternalServerError };
        }
}
