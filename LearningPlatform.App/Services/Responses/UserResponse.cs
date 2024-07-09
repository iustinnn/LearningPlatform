namespace LearningPlatform.App.Services.Responses
{
    public class UserResponse
    {
        public Guid? UserId { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
