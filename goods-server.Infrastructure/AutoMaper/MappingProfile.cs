using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.AutoMaper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // ACCOUNT
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, GetAccountDTO>().ReverseMap();
            CreateMap<RegisterDTO, Account>();

            // REPLYCOMMENT
            CreateMap<CreateReplyDTO, ReplyComment>();
            CreateMap<ReplyComment, GetReplyCommentDTO>().ReverseMap();

            // Category Mapping
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();

            // City Mapping
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<CreateCityDTO, City>();
            CreateMap<UpdateCityDTO, City>();
            // Genre Mapping
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<CreateGenreDTO, Genre>();
            CreateMap<UpdateGenreDTO, Genre>();
            /// OrderDetail Mapping
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<CreateOrderDetailDTO, OrderDetail>();
            CreateMap<UpdateOrderDetailDTO, OrderDetail>();
            // Rating Mapping
            CreateMap<Rating, RatingDTO>().ReverseMap();
            CreateMap<CreateRatingDTO, Rating>();
            CreateMap<UpdateRatingDTO, Rating>();
        }
    }
}
