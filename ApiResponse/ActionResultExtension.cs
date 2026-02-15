using Microsoft.AspNetCore.Mvc;

namespace AthenaUnionLibrary.ApiResponse
{
    public static class ActionResultExtension
    {
        public static IActionResult AsResult<T>(this AthenaApiResponse<T> data)
        {
            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }
    }
}