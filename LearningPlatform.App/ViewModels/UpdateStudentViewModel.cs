﻿namespace LearningPlatform.App.ViewModels
{
    public class UpdateStudentViewModel
    {
        public Guid StudentId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? PhoneNumber {  get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; } = string.Empty;
        public string? Description {  get; set; } = string.Empty;


    }
}
