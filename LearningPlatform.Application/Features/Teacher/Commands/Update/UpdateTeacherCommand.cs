using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Teacher.Commands.Update
{
    public class UpdateTeacherCommand : IRequest<UpdateTeacherCommandResponse>
    {
        private UpdateTeacherCommand()
        {
            // Private constructor to prevent direct instantiation
        }

        public Guid TeacherId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;


        public string? PhoneNumber { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? ProfilePictureUrl { get; set; } = string.Empty;

        [JsonConstructor]
        public UpdateTeacherCommand( string firstName, string lastName, string phoneNumber, string description, string profilePictureUrl)
        {
  
            FirstName = firstName;
            LastName = lastName;   
            PhoneNumber = phoneNumber;
            Description = description;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
