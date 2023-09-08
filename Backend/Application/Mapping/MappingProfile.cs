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

            CreateMap<FlashCardWord, GetFlashCardsWordDto>();

            CreateMap<FlashCardSet, GetFlashCardsSetDto>()
                .ForMember(d => d.Words, o => o.MapFrom(s => s.Words.Take(maximumNumberOfWords)));
        }
    }
}