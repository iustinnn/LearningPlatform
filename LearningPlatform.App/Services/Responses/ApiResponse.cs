namespace LearningPlatform.App.Services.Responses
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public List<string> Errors { get; set; }
    }

}