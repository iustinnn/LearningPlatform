using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Domain.Entities
{
    public class StudentCourse : AuditableEntity
    {

        private StudentCourse(Guid studentId, Guid courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }

        public static Result<StudentCourse> Create(Guid studentId, Guid courseId)
        {

            if (studentId == Guid.Empty)
            {
                return Result<StudentCourse>.Failure("Student ID is required");
            }
            if (courseId == Guid.Empty)
            {
                return Result<StudentCourse>.Failure("Course ID is required");
            }
            return Result<StudentCourse>.Success(new StudentCourse(studentId, courseId));
        }

        public Guid StudentId { get; set; }

        public Guid CourseId { get; set; }

        public int Score { get; set; } = 0;


    }
}
