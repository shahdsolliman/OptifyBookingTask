using AutoMapper;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Application.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Reservation -> ToReturnDto
            CreateMap<Reservation, ReservationToReturnDto>()
                .ForMember(dest => dest.TripName, opt => opt.MapFrom(src => src.Trip.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Trip.CityName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));

            // ReservationCreateDto -> Reservation
            CreateMap<ReservationCreateDto, Reservation>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ReverseMap();

            // ReservationUpdateDto -> Reservation
            CreateMap<ReservationUpdateDto, Reservation>().ReverseMap();

            // Reservation -> internal DTO (optional)
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.TripName, opt => opt.MapFrom(src => src.Trip.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Trip.CityName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<Trip, TripDto>().ReverseMap();
        }
    }
}
