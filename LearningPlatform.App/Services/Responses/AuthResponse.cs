﻿namespace LearningPlatform.App.Services.Responses
{
    public class AuthResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public string? ValidationErrors { get; set; }
        public bool IsSuccess { get; set; }

        public T? Data { get; set; }
    }
}
