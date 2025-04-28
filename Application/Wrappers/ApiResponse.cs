namespace inventory_api.Application.Wrappers
{
    public enum ApiStatusCode
    {
        Ok = 200,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
        public ApiStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(T data, string message = "Request was successful", ApiStatusCode statusCode = ApiStatusCode.Ok)
        {
            Success = true;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        public ApiResponse(string message, ApiStatusCode statusCode = ApiStatusCode.BadRequest, List<string>? errors = null)
        {
            Success = false;
            Message = message;
            Data = default!;
            StatusCode = statusCode;
            Errors = errors ?? new List<string>();
        }
    }
}
