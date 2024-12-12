#nullable disable

using AutoMapper;
using MediViewerLite.Data;
using MediViewerLite.Dto;

namespace MediViewerLite.IngestionWorker.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<ActivityAuditTrail, ActivityAuditTrailDto>().ReverseMap();
            CreateMap<AuditDocument, AuditDocumentDto>().ReverseMap();
            CreateMap<AuditEntity, AuditEntityDto>().ReverseMap();
            CreateMap<BatchSubmissionDocumentSource, BatchSubmissionDocumentSourceDto>().ReverseMap();
            CreateMap<BatchSubmissionDocumentSourceConfig, BatchSubmissionDocumentSourceConfigDto>().ReverseMap();
            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<Entity, EntityDto>().ReverseMap();
            CreateMap<Ingestion, IngestionDto>().ReverseMap();
            CreateMap<Submission, SubmissionDto>().ReverseMap();
            CreateMap<StagingSubmission, StagingSubmissionDto>().ReverseMap();
            CreateMap<StagingIngestion, StagingIngestionDto>().ReverseMap();
            CreateMap<StagingDocument, StagingDocumentDto>().ReverseMap();
            CreateMap<StagingEntity, StagingEntityDto>().ReverseMap();
        }
    }
}
