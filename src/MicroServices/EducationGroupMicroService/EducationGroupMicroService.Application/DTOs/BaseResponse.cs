namespace EducationGroupMicroService.Application.DTOs
{
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
