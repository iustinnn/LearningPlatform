using MediatR;


namespace LearningPlatform.Application.Features.Student.Commands.Create
{
        public class CreateStudentCommand : IRequest<CreateStudentCommandResponse>
        {
            public string FirstName { get; set; } = default!;

            public string LastName { get; set; } = default!;

            public string Email { get; set; } = default!;

            public string ProfilePictureUrl { get; set; } = default!;
         


    }

}
