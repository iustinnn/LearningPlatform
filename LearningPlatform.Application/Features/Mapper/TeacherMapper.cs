using AutoMapper;
using LearningPlatform.Application.Features.Student.Queries;
using LearningPlatform.Application.Features.Student.Commands.Update;
using Domain;
using LearningPlatform.Application.Features.Student.Commands.Create;
using LearningPlatform.Application.Features.Teacher.Commands.Create;
using LearningPlatform.Application.Features.Teacher.Commands.Update;


namespace LearningPlatform.Application.Features.Mapper
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<CreateTeacherCommand, Domain.Entity.Teacher>()
         
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl));

            // Mapping for UpdateTeacherCommand
            CreateMap<UpdateTeacherCommand, Domain.Entity.Teacher>()
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.TeacherId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl));

        }
    }
}
