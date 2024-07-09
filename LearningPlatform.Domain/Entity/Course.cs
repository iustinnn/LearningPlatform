using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Domain.Entities
{
    public class Course : AuditableEntity
    {

        private Course(string title, string description, DateTime startDate, DateTime endDate, Guid teacherId, string videoUrl, string thumbnail)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            TeacherId = teacherId;
            VideoUrl = videoUrl;
            Students = new List<Student>();
            Thumbnail = thumbnail;
        }

        public static Result<Course> Create(string title, string description, DateTime startDate, DateTime endDate, Guid teacherId, string videoUrl, string thumbnail)
        {
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(description))
            {
                return Result<Course>.Failure("Course title and description are required");
            }
            return Result<Course>.Success(new Course(title, description, startDate, endDate, teacherId,videoUrl, thumbnail));
        }

        [Key]
        public Guid CourseId { get; private set; }


        public string Title { get; private set; } 

      
        public string Description { get; private set; }

        public string VideoUrl { get; private set; }

        public string Thumbnail { get; private set; }

     
        public Guid TeacherId { get; private set; }

        public Teacher Teacher { get; private set; }

       
        public ICollection<Student> Students { get; private set; }

       
        public DateTime StartDate { get; private set; }

       
        public DateTime EndDate { get; private set; }

        // Method to assign a teacher to this course
        public void AssignTeacher(Teacher teacher)
        {
            Teacher = teacher;
        }

        // Method to enroll a student in this course
        public void EnrollStudent(Student student)
        {
            Students.Add(student);
        }

        // Method to remove a student from this course
        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

        // Method to update the Video URL
        public void UpdateVideoUrl(string videoUrl)
        {
            VideoUrl = videoUrl;
        }
    }
}
