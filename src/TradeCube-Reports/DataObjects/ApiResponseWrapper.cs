using TradeCube_Reports.Messages;

namespace TradeCube_Reports.DataObjects
{
    public class ApiResponseWrapper<T> : ApiResponse
    {
        public int? RecordCount { get; set; }
        public T Data { get; }

        public ApiResponseWrapper()
        {
        }

        public ApiResponseWrapper(T data)
        {
            Data = data;
        }

        public ApiResponseWrapper(string status, T data)
        {
            Status = status;
            Data = data;
        }
    }
}
