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
                        Name = "First set",
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
                            },
                            new FlashCardWord
                            {
                                Word = "Dzien dobry",
                                Translation = "Good morning"
                            },
                            new FlashCardWord
                            {
                                Word = "Mały",
                                Translation = "Small"
                            },
                            new FlashCardWord
                            {
                                Word = "Duży",
                                Translation = "Big"
                            },
                            new FlashCardWord
                            {
                                Word = "Proszę",
                                Translation = "Please"
                            }
                        }
                    },
                    new FlashCardSet()
                    {
                        Name = "Set 2",
                        Words = new FlashCardWord[]
                        {
                            new FlashCardWord
                            {
                                Word = "Ulcie",
                                Translation = "Street"
                            },
                            new FlashCardWord
                            {
                                Word = "Stary",
                                Translation = "Old"
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