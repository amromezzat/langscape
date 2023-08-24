using System.Linq;
using Application.Features.FlashCards.Queries.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        private const int PreviewWordsCount = 5;

        public MappingProfile()
        {
            CreateMap<FlashCardSet, FlashCardSetDto>()
                .ForMember(d => d.Words, o => o.MapFrom(s => s.Words.Take(PreviewWordsCount)));
        }
    }
}