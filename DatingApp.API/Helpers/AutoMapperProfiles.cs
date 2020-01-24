using AutoMapper;
using DatingApp.API.Models;
using DatingApp.API.ViewModels;
using System.Linq;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForSeedViewModel, User>();
            CreateMap<PhotoViewModel, Photo>();
            CreateMap<User, UserDetailsViewModel>()
                    .ForMember(dest => dest.PhotoUrl, options =>
                    {
                        options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                    })
                    .ForMember(dest => dest.Age, options =>
                    {
                         options.MapFrom(src => src.DateOfBirth.Age());
                    });
            CreateMap<Photo, PhotoDetailsViewModel>();
            CreateMap<User, UserViewModel>()
                    .ForMember(dest => dest.PhotoUrl, options =>
                    {
                        options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                    })
                    .ForMember(dest => dest.Age, options =>
                    {
                        options.MapFrom(src => src.DateOfBirth.Age());
                    });
        }
    }
}
