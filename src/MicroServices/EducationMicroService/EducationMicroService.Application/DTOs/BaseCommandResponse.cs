namespace EducationMicroService.Application.DTOs
{
    //public class BaseCommandResponse
    //{
    //    public BaseCommandResponse()
    //    {
    //        this.Message = string.Empty;
    //        this.Errors = new List<string>();
    //    }

    //    public int Id { get; set; }
    //    public bool Success { get; set; }
    //    public string Message { get; set; }
    //    public List<string> Errors { get; set; }
    //}

    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }

    public class BaseResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
