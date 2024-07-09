using AutoMapper;
using LearningPlatform.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.StudentCourse.Commands.Create
{
    public class CreateStudentCourseCommandHandler : IRequestHandler<CreateStudentCourseCommand, CreateStudentCourseCommandResponse>
    {
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateStudentCourseCommandHandler(IStudentCourseRepository studentCourseRepository, IMapper mapper, ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            _studentCourseRepository = studentCourseRepository;
            _mapper = mapper;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        public async Task<CreateStudentCourseCommandResponse> Handle(CreateStudentCourseCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateStudentCourseCommandValidator(_studentRepository, _courseRepository, _studentCourseRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateStudentCourseCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var student = await _studentRepository.FindByIdAsync(request.StudentId);
            if (student == null)
            {
                return new CreateStudentCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Student not found" }
                };
            }

            var course = await _courseRepository.FindByIdAsync(request.CourseId);
            if (course == null)
            {
                return new CreateStudentCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Course not found" }
                };
            }

            var studentCourse = Domain.Entities.StudentCourse.Create(student.Value.UserId, course.Value.CourseId);
            if (!studentCourse.IsSuccess)
            {
                return new CreateStudentCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { studentCourse.Error }
                };
            }

            await _studentCourseRepository.AddAsync(studentCourse.Value);

            return new CreateStudentCourseCommandResponse
            {
                Success = true,
                StudentCourse = new CreateStudentCourseDto
                {
                    StudentId = studentCourse.Value.StudentId,
                    CourseId = studentCourse.Value.CourseId
                }
            };
        }   
    }
}
