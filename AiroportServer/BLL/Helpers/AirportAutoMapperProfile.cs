using AutoMapper;
using BLL.Models.Dtos;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public class AirportAutoMapperProfile:Profile
    {
        public AirportAutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User,UserShortDto>().ReverseMap();
            CreateMap<UserToken, UserTokenDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer,CustomerShortDto>().ReverseMap();
            CreateMap<Employee,EmployeeDto>().ReverseMap();
            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<Flight,FlightShortDto>().ReverseMap();
            CreateMap<Gate,GateDto>().ReverseMap();
            CreateMap<Luggage, LuggageDto>().ReverseMap();
            CreateMap<Plane,PlaneDto>().ReverseMap();  
            CreateMap<Terminal,TerminalDto>().ReverseMap();
            CreateMap<Terminal,TerminalShortDto>().ReverseMap();
            
        }
    }
}
