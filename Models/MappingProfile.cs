using AutoMapper;
using web_rest_hudz_kp21.Models.DTOs;

namespace web_rest_hudz_kp21.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BicycleDTO, Bicycle>();
            CreateMap<Bicycle, BicycleSummaryDTO>();
            CreateMap<BikePartDTO, BikePart>();
            CreateMap<BikePart, BikePartSummaryDTO>();
        }
    }
}