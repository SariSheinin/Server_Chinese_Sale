using AutoMapper;
using Chinese_Sale.DAL;
using Chinese_Sale.DTOs;
using Chinese_Sale.Models;
using Chinese_Sale.Models.DTOs;
using sale_server.Models;

namespace Chinese_Sale
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserLoginDTO, User>().ReverseMap();
            CreateMap<DonorDTO, Donor>().ReverseMap();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            //.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //.ForMember(dest => dest.TypeOfDonation, opt => opt.Ignore())
            //.ForMember(dest => dest.DonationsList, opt => opt.Ignore());
            CreateMap<PresentDTO, Present>().ReverseMap();
            CreateMap<UserRegisterDTO, User>().ReverseMap();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.CardPrice, opt => opt.MapFrom(src => src.CardPrice))
            //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            //.ForMember(dest => dest.DonorId, opt => opt.MapFrom(src => src.DonorId))
            //.ForMember(dest => dest.Donor, opt => opt.MapFrom(src => src.DonorId.CompareTo(Ge));
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<RaffleDTO, Raffle>().ReverseMap();
            CreateMap<PresentsOrderDTO, PresentsOrder>();
            CreateMap<PresentsOrder, PresentOrderDTOWithPresent>();




        }
    }
}
