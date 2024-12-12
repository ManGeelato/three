#nullable disable

using AutoMapper;
using ThreeSixty.Application.Incident.Commands;
using ThreeSixty.Application.Entity.Commands;
using ThreeSixty.Data;
using ThreeSixty.Dto;

namespace ThreeSixty.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            //CreateMap<ActivityAuditTrail, ActivityAuditTrailDto>().ReverseMap();
            CreateMap<Incident, IncidentDto>().ReverseMap();
            CreateMap<Entity, EntityDto>().ReverseMap();
            CreateMap<Suburb, SuburbDto>().ReverseMap();
            CreateMap<IncidentStatus, IncidentStatusDto>().ReverseMap();

            //CreateCommand Mappings

            CreateMap<CreateIncidentCommand, IncidentDto>();
            CreateMap<CreateEntityCommand, EntityDto>();

            //UpdateCommand Mappings

            CreateMap<UpdateIncidentCommand, IncidentDto>();
            CreateMap<UpdateEntityCommand, EntityDto>();
        }
    }
}
