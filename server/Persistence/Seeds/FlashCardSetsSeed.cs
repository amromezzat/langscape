using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.Seeds
{
    public static class FlashCardSetsSeed
    {
        public static async Task SeedFlashCardSets(IUnitOfWork unitOfWork)
        {
            var flashCardsRepository = unitOfWork.GetRepository<FlashCardsSet>();

            if(!await flashCardsRepository.Entities.AnyAsync())
            {
                var flashCards = new FlashCardsSet[]
                {
                    new FlashCardsSet()
                    {
                        Name = "First set",
                        Words = new FlashCardsWord[] 
                        {
                            new FlashCardsWord
                            {
                                Word = "Czarny",
                                Translation = "Black"
                            },
                            new FlashCardsWord
                            {
                                Word = "Kot",
                                Translation = "Cat"
                            },
                            new FlashCardsWord
                            {
                                Word = "Dzien dobry",
                                Translation = "Good morning"
                            },
                            new FlashCardsWord
                            {
                                Word = "Mały",
                                Translation = "Small"
                            },
                            new FlashCardsWord
                            {
                                Word = "Duży",
                                Translation = "Big"
                            },
                            new FlashCardsWord
                            {
                                Word = "Proszę",
                                Translation = "Please"
                            }
                        }
                    },
                    new FlashCardsSet()
                    {
                        Name = "Set 2",
                        Words = new FlashCardsWord[]
                        {
                            new FlashCardsWord
                            {
                                Word = "Ulcie",
                                Translation = "Street"
                            },
                            new FlashCardsWord
                            {
                                Word = "Stary",
                                Translation = "Old"
                            },
                            new FlashCardsWord
                            {
                                Word = "Nowy",
                                Translation = "New"
                            }
                        }
                    }
                };

                await flashCardsRepository.AddRangeAsync(flashCards);
            }
        }
    }
}