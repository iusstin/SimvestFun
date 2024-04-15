using AutoMapper;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Models;

namespace SimvestFun.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, AuthenticateRequest>().ReverseMap();
            CreateMap<ApplicationUser, UserModel>().ReverseMap();
            CreateMap<RegisterRequest, ApplicationUser>().ReverseMap();
            CreateMap<UserStock, UserStockModel>().ReverseMap();
            CreateMap<StockPrice, StockPriceModel>().ReverseMap();
            CreateMap<UserAction, UserActionModel>().ReverseMap();
        }
    }
}
