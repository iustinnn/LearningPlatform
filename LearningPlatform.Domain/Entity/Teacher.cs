using Domain.Entity;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Domain.Entity;

public class Teacher : AuditableEntity
{
    private Teacher()
    {
    }

    private Teacher(string firstName, string lastName, string email, string phoneNumber, string description, string profilePictureUrl)
    {
    
   
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Description = description;
        ProfilePictureUrl = profilePictureUrl;
      //  Speciality = speciality;
    }
    private Teacher(string firstName, string lastName, string email)
    {


        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
   
    [Key] public Guid TeacherId { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? PhoneNumber { get; private set; }

    public string? Description { get; private set; }

    public string? ProfilePictureUrl { get; private set; }



    //public Speciality Speciality { get; private set; }



    public static Result<Teacher> Create( string firstName, string lastName, string email, string phoneNumber, string description, string profilePictureUrl)
    {
        return Result<Teacher>.Success(new Teacher( firstName, lastName, email, phoneNumber, description, profilePictureUrl));
    }

    public static Result<Teacher> Create(string firstName, string lastName, string email)
    {
        return Result<Teacher>.Success(new Teacher(firstName, lastName, email));
    }


}