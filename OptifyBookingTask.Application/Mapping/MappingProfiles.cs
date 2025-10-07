using AutoMapper;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Application.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Reservation, ReservationToReturnDto>()
                .ForMember(dest => dest.TripName, opt => opt.MapFrom(src => src.Trip.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Trip.CityName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<ReservationCreateDto, Reservation>();
            CreateMap<ReservationUpdateDto, Reservation>();
        }
    }
}
