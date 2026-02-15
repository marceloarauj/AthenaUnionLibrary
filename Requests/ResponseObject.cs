using System.Net;

namespace AthenaUnionLibrary.Requests
{
    public record ResponseObject<TResponse>
    {
        public ResponseObject(HttpResponseMessage response, TResponse? responseObject)
        {
            StatusCode = response.StatusCode;
            Success = response.IsSuccessStatusCode;
            Response = responseObject;
        }

        public ResponseObject(HttpStatusCode response, string error) 
        {
            StatusCode = response;
            Success = false;
            Error = error;
        }

        public HttpStatusCode StatusCode;
        public bool Success;
        public TResponse? Response;
        public string? Error;
    }
}
