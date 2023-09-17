using System.Linq;
using Application.Features.FlashCards.Queries.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var maximumNumberOfWords = int.MaxValue;

            CreateMap<FlashCardsWord, GetFlashCardsWordDto>();

            CreateMap<FlashCardsSet, GetFlashCardsSetDto>()
                .ForMember(d => d.Words, o => o.MapFrom(s => s.Words.Take(maximumNumberOfWords)))
                .ForMember(d => d.Meta, o => o.MapFrom(s => new GetFlashCardsSetDto.MetaData()
                {
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedById
                }));
        }
    }
}