using AutoMapper;
using SportLize.Profile.Api.Profile.Repository.Model;
using SportLize.Profile.Api.Profile.Shared.Dto;

namespace SportLize.Profile.Api.Profile.Business.Profiles
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserWriteDto, User>();
            CreateMap<User, UserWriteDto>();
            CreateMap<UserReadDto, User>();
            CreateMap<User, UserReadDto>();

            CreateMap<PostWriteDto, Post>()
            .ForMember(destination=> destination.Media, options=> options.MapFrom(src=> Convert.FromBase64String(src.Media64)));
            CreateMap<Post, PostWriteDto>()
            .ForMember(destination => destination.Media64, options => options.MapFrom(src => Convert.ToBase64String(src.Media)));
            CreateMap<PostReadDto, Post>()
            .ForMember(destination => destination.Media, options => options.MapFrom(src => Convert.FromBase64String(src.Media64)));
            CreateMap<Post, PostReadDto>()
            .ForMember(destination => destination.Media64, options => options.MapFrom(src => Convert.ToBase64String(src.Media)));

            CreateMap<CommentWriteDto, Comment>();
            CreateMap<Comment, CommentWriteDto>();
            CreateMap<CommentReadDto, Comment>();
            CreateMap<Comment, CommentReadDto>();
        }
    }
}
