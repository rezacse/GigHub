using AutoMapper;
using GigHub.Core.Dto;
using GigHub.Core.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public static IMapper _mapper;
        public MappingProfile()
        {

            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<ApplicationUser, UserDto>();
                c.CreateMap<Gig, GigDto>();
                c.CreateMap<Notification, NotificationDto>();
            });

            _mapper = config.CreateMapper();
            //Mapper.Configuration.CompileMappings();
        }







    }
}