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
    public class RemoveSetFromFavoriteCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    internal class RemoveSetFromFavoriteCommandHandler : IRequestHandler<RemoveSetFromFavoriteCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;

        public RemoveSetFromFavoriteCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(RemoveSetFromFavoriteCommand command, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.GetUserId();
            await _unitOfWork.GetRepository<FlashCardSetFavorite>().DeleteByIdAsync(userId, command.Id);
            await _unitOfWork.Save(cancellationToken);

            return Result<Unit>.Success("Set have been removed from favorites.");
        }
    }
}