using AutoMapper;
using LearningPlatform.Application.Features.Student.Queries;
using LearningPlatform.Application.Features.Student.Commands.Update;
using Domain;
using LearningPlatform.Application.Features.Student.Commands.Create;
using LearningPlatform.Application.Features.Teacher.Commands.Update;
using Domain.AuthEntity;
using Domain.Entity;
using System.Globalization;
using LearningPlatform.Application.Features.Teacher;
using LearningPlatform.Application.Features.StudentCourse.Commands.Update;


namespace LearningPlatform.Application.Features.Mapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {


            CreateMap<UpdateStudentCommand, Domain.Entities.Student>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName,
                    opt => opt.Condition((src, dest, sourceMember) => sourceMember != null || dest.FirstName == null))
                .ForMember(dest => dest.LastName,
                    opt => opt.Condition((src, dest, sourceMember) => sourceMember != null || dest.LastName == null));



            CreateMap<Domain.Entities.StudentCourse, UpdateStudentCourseDto>()
               .ReverseMap();

            CreateMap<Domain.Entities.Student, UpdateStudentDto>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));


         
            CreateMap<Domain.Entities.Student, StudentDto>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl));
                

        }



    }
    }
