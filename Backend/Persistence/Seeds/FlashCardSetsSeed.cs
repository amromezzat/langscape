using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.Seeds
{
    public static class FlashCardSetsSeed
    {
        public static async Task SeedFlashCardSets(IUnitOfWork unitOfWork)
        {
            var flashCardsRepository = unitOfWork.GetRepository<FlashCardSet>();

            if(!await flashCardsRepository.Entities.AnyAsync())
            {
                var flashCards = new FlashCardSet[]
                {
                    new FlashCardSet()
                    {
                        Words = new FlashCardWord[] 
                        {
                            new FlashCardWord
                            {
                                Word = "Czarny",
                                Translation = "Black"
                            },
                            new FlashCardWord
                            {
                                Word = "Kot",
                                Translation = "Cat"
                            }
                        }
                    },
                    new FlashCardSet()
                    {
                        Words = new FlashCardWord[]
                        {
                            new FlashCardWord
                            {
                                Word = "Ulcie",
                                Translation = "Street"
                            },
                            new FlashCardWord
                            {
                                Word = "Old",
                                Translation = "Stary"
                            },
                            new FlashCardWord
                            {
                                Word = "Nowy",
                                Translation = "New"
                            }
                        }
                    }
                };

                await flashCardsRepository.AddRange(flashCards);
            }
        }
    }
}