using MediatR;

namespace LearningPlatform.Application.Features.Course.Commands.Create
{
    public class CreateCourseCommand : IRequest<CreateCourseCommandResponse>
    {

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; } = default!;
        public DateTime EndDate { get; set; } = default!;
        public Guid TeacherId { get; set; }
        public string VideoUrl { get; set; } = default!;

        public string Thumbnail { get; set; } = default!;

    }
}
