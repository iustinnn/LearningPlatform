using AutoMapper;
using LearningPlatform.Application.Features.Course.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Mapper
{
    public class CourseProfile : Profile
    {

        public CourseProfile()
        {
            CreateMap<UpdateCourseCommand, Domain.Entities.Course>()
              .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
              .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl));
        }
    }
}
