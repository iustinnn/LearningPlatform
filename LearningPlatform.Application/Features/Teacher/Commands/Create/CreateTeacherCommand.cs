using System.Text.Json.Serialization;
using MediatR;

namespace LearningPlatform.Application.Features.Teacher.Commands.Create;

public class CreateTeacherCommand : IRequest<CreateTeacherCommandResponse>
{
  
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? Description { get; set; }
    public string? ProfilePictureUrl { get; set; }




}