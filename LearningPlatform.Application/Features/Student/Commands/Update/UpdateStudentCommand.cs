using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Student.Commands.Update
{
    public class UpdateStudentCommand : IRequest<UpdateStudentCommandResponse>
    {
        private UpdateStudentCommand()
        {
           
        }

        public Guid StudentId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? ProfilePictureUrl { get; set; } = string.Empty;




        [JsonConstructor]
        public UpdateStudentCommand(Guid studentId, string? firstName, string? lastName, string? phoneNumber, string? description)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Description = description;

       
        }
    }
}
