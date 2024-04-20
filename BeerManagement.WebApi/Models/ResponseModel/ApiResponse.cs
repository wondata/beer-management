using Microsoft.AspNetCore.Routing.Constraints;

namespace BeerManagement.WebApi.Models.ResponseModel
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public string ResponseStatus { get; set; }

        public ApiResponse(T t, bool isError = false, string msg = null)
        {
            Data = t;
            ResponseStatus = isError ? ResponseStatusEnum.Error.ToString() : ResponseStatusEnum.Success.ToString();
            Message = msg;
        }

        public ApiResponse(bool isError = false, string msg = null)
        {
            Data = default(T);
            ResponseStatus = isError ? ResponseStatusEnum.Error.ToString() : ResponseStatusEnum.Success.ToString();
            Message = msg;
        }
    }
}
