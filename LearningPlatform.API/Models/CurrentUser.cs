﻿namespace LearningPlatform.API.Models
{
    public class CurrentUser
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }

        public string UserId { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
