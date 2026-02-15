using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AthenaUnionLibrary.Requests
{
    public static class AthenaRequest
    {

        public static async Task<ResponseObject<TResponse>> Post<TRequest, TResponse>
        (
            this HttpClient httpClient, 
            string url,
            TRequest requestObject
        )
        {
            return await SendRequestAsync<TResponse>(async () =>
            {
                if (requestObject == null) throw new ArgumentNullException(nameof(requestObject));

                var json = JsonSerializer.Serialize(requestObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                return await httpClient.PostAsync(url, content);
            });
        }

        public static async Task<ResponseObject<TResponse>> Put<TRequest, TResponse>
        (
            this HttpClient httpClient,
            string url,
            TRequest requestObject
        )
        {
            return await SendRequestAsync<TResponse>(async () =>
            {
                if (requestObject == null) throw new ArgumentNullException(nameof(requestObject));

                var json = JsonSerializer.Serialize(requestObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                return await httpClient.PutAsync(url, content);
            });
        }

        public static async Task<ResponseObject<TResponse>> Patch<TRequest, TResponse>
        (
            this HttpClient httpClient,
            string url,
            TRequest requestObject
        )
        {
            return await SendRequestAsync<TResponse>(async () =>
            {
                if (requestObject == null) throw new ArgumentNullException(nameof(requestObject));

                var json = JsonSerializer.Serialize(requestObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                return await httpClient.PatchAsync(url, content);
            });
        }

        public static async Task<ResponseObject<TResponse>> Get<TResponse>(this HttpClient httpClient, string url)
        {
            return await SendRequestAsync<TResponse>(async () =>
            {
                return await httpClient.GetAsync(url);
            });
        }

        public static async Task<ResponseObject<TResponse>> Delete<TResponse>(this HttpClient httpClient, string url)
        {
            return await SendRequestAsync<TResponse>(async () =>
            {
                return await httpClient.DeleteAsync(url);
            });
        }

        public static async Task<ResponseObject<TResponse>> PostFormUrlEncoded<TRequest, TResponse>
        (
            this HttpClient httpClient,
            string url,
            TRequest requestObject
        )
        {
            return await SendRequestAsync<TResponse>(async () =>
            {
                if(requestObject == null) throw new ArgumentNullException(nameof(requestObject));

                var formData = MakeFormData(requestObject);

                var content = new FormUrlEncodedContent(formData);

                return await httpClient.PostAsync(url, content);
            });
        }

        public static async Task<ResponseObject<TResponse>> PutFormUrlEncoded<TRequest, TResponse>
        (
            this HttpClient httpClient,
            string url,
            TRequest requestObject
        )
        {
            return await SendRequestAsync<TResponse>(async () =>
            {
                if (requestObject == null) throw new ArgumentNullException(nameof(requestObject));

                var formData = MakeFormData(requestObject);

                var content = new FormUrlEncodedContent(formData);

                return await httpClient.PutAsync(url, content);
            });
        }

        public static async Task<ResponseObject<TResponse>> PatchFormUrlEncoded<TRequest, TResponse>
        (
            this HttpClient httpClient,
            string url,
            TRequest requestObject
        )
        {
            return await SendRequestAsync<TResponse>(async () =>
            {
                if (requestObject == null) throw new ArgumentNullException(nameof(requestObject));

                var formData = MakeFormData(requestObject);

                var content = new FormUrlEncodedContent(formData);

                return await httpClient.PatchAsync(url, content);
            });
        }

        public static async Task<ResponseObject<TResponse>> SendRequestAsync<TResponse>(Func<Task<HttpResponseMessage>> function)
        {
            HttpResponseMessage? responseMessage = null;
            try
            {
                responseMessage = await function.Invoke();

                responseMessage.EnsureSuccessStatusCode();

                var responseObject = await responseMessage.Content.ReadFromJsonAsync<TResponse>();

                return new ResponseObject<TResponse>(responseMessage, responseObject);
            }
            catch (Exception ex) 
            {
                return new ResponseObject<TResponse>(responseMessage?.StatusCode ?? HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private static Dictionary<string, string> MakeFormData<TRequest>(TRequest requestObject)
        {
            var dictionary = new Dictionary<string, string>();

            foreach(var property in typeof(TRequest).GetProperties())
            {
                if (property.PropertyType != typeof(string)) continue;

                var attribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();

                var propertyName = property.Name;

                if (attribute != null) propertyName = attribute.Name;

                var value = property.GetValue(requestObject);

                if (value != null) dictionary[propertyName] = value.ToString()!;
            }

            return dictionary;
        }
    }
}
