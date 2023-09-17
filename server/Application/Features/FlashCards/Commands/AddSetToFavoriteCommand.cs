using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Langscape.Shared.Implementation;
using MediatR;
using Persistence.Repositories;

namespace Application.Features.FlashCards.Commands
{
    public class AddSetToFavoriteCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    internal class AddSetToFavoriteCommandHandler : IRequestHandler<AddSetToFavoriteCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;

        public AddSetToFavoriteCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(AddSetToFavoriteCommand command, CancellationToken cancellationToken)
        {
            var set = await _unitOfWork.GetRepository<FlashCardsSet>().GetByIdAsync(command.Id);
            if (set == null)
            {
                return Result<Unit>.Failure($"Set doesn't exist.");
            }

            var userId = _userAccessor.GetUserId();
            var favorite = await _unitOfWork.GetRepository<FlashCardSetFavorite>().GetByIdAsync(userId, command.Id);
            if (favorite != null)
            {
                return Result<Unit>.Failure($"Set is already added to favorites.");
            }

            await _unitOfWork.GetRepository<FlashCardSetFavorite>()
                .AddAsync(new FlashCardSetFavorite()
                {
                    AppUserId = userId,
                    SetId = command.Id
                });
            await _unitOfWork.Save(cancellationToken);

            return Result<Unit>.Success("Set have been added to favorites.");
        }
    }
}