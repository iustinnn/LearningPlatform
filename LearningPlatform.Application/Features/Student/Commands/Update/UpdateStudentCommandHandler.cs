using MediatR;
using AutoMapper;
using LearningPlatform.Application.Persistence;



namespace LearningPlatform.Application.Features.Student.Commands.Update
{
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateStudentCommand, UpdateStudentCommandResponse>
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper _mapper;

        public UpdateTeacherCommandHandler(IStudentRepository StudentRepository, IMapper mapper)
        {
            studentRepository = StudentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UpdateStudentCommandResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateStudentCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateStudentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var existingStudent = await studentRepository.FindByIdAsync(request.StudentId);
            if (!existingStudent.IsSuccess)
            {
                return new UpdateStudentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Student not found." }
                };
            }

            var updatedStudent = _mapper.Map(request, existingStudent.Value);
            var result = await studentRepository.UpdateAsync(updatedStudent);

            if (!result.IsSuccess)
            {
                return new UpdateStudentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }

            return new UpdateStudentCommandResponse
            {
                Success = true,
                StudentDTO = new UpdateStudentDto()
                {
                    
                    StudentId = updatedStudent.UserId,
                    FirstName = updatedStudent.FirstName,
                    LastName = updatedStudent.LastName,
                    PhoneNumber = updatedStudent.PhoneNumber,
                    Description = updatedStudent.Description,
                    ProfilePictureUrl = updatedStudent.ProfilePictureUrl
                    /*
                    Email = updatedStudent.Email,
                    PhoneNumber = updatedStudent.PhoneNumber,
                    Description = updatedStudent.Description
                    */
               

                            
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
