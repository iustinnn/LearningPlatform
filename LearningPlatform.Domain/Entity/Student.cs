using LearningPlatform.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Domain.Entities
{
    public class Student : AuditableEntity
    {
        private Student(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;

        }

        //[Key] public Guid StudentId { get; private set; }


        [Key] public Guid UserId { get; private set; }

        public string FirstName { get; private set; } = string.Empty;

        public string LastName { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string PhoneNumber { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public string ProfilePictureUrl { get; private set; } = string.Empty;   

        public ICollection<Course> Courses { get; private set; }


        public static Result<Student> Create(string studentName, string lastName, string email)
        {
            if(string.IsNullOrEmpty(studentName)&&string.IsNullOrEmpty(lastName))
            {
                return Result<Student>.Failure("Student name is required");
            }
            return Result<Student>.Success(new Student(studentName,lastName,email));
        }

       

        
    }
}
