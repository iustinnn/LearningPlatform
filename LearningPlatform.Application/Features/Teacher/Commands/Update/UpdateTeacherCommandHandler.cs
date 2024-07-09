using MediatR;
using AutoMapper;
using LearningPlatform.Application.Persistence;

namespace LearningPlatform.Application.Features.Teacher.Commands.Update
{
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, UpdateTeacherCommandResponse>
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper _mapper;

        public UpdateTeacherCommandHandler(ITeacherRepository TeacherRepository, IMapper mapper)
        {
            teacherRepository = TeacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UpdateTeacherCommandResponse> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTeacherCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateTeacherCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var existingTeacher = await teacherRepository.FindByIdAsync(request.TeacherId);
            if (!existingTeacher.IsSuccess)
            {
                return new UpdateTeacherCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Teacher not found." }
                };
            }

            var updatedTeacher = _mapper.Map(request, existingTeacher.Value);
            var result = await teacherRepository.UpdateAsync(updatedTeacher);

            if (!result.IsSuccess)
            {
                return new UpdateTeacherCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }

            return new UpdateTeacherCommandResponse
            {
                Success = true,
                TeacherDTO = new UpdateTeacherDto()
                {

                    TeacherId = updatedTeacher.TeacherId,
                    FirstName = updatedTeacher.FirstName,
                    LastName = updatedTeacher.LastName,
                    Email = updatedTeacher.Email,
                    PhoneNumber = updatedTeacher.PhoneNumber,
                    Description = updatedTeacher.Description,
                    ProfilePictureUrl = updatedTeacher.ProfilePictureUrl

                   
                   

                    /*
                    FirstName = updatedTeacher.FirstName,
                    LastName = updatedTeacher.LastName,
                    Email = updatedTeacher.Email,
                    Gender = updatedTeacher.Gender.ToString(),
                    Speciality = updatedTeacher.Speciality,
                    BirthDate = updatedTeacher.BirthDate.ToString("dd-MM-yyyy")
                    */
                }
            };
        }
    }
}
