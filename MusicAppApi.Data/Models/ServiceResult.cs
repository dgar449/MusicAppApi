namespace MusicAppApi.Data.Models
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get { return string.IsNullOrEmpty(ErrorMessage); } private set { } }
        public string ErrorMessage { get; set; }
        public T Data { get; private set; }

        public ServiceResult(T data)
        {
            Data = data;
        }

        public ServiceResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}