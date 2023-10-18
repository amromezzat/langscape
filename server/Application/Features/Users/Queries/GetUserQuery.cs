using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Services;
using Application.Services;
using Application.Users.Dto;
using Domain.Entities;
using Langscape.Shared;
using Langscape.Shared.Implementation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Users.Queries
{
    public class GetUserQuery : IRequest<ActionResult<IResult<UserDto>>>
    {
        public string UserId { get; set; }
        public string Username { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ActionResult<IResult<UserDto>>>
    {
        private readonly IUserManager _userManager;
        private readonly IUserService _userService;

        public GetUserQueryHandler(IUserManager userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<ActionResult<IResult<UserDto>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            AppUser user = null;
            
            if (request.UserId != null) 
            {
                user = await _userManager.GetUserById(request.UserId);
            }
            else if(request.Username != null) 
            {
                user = await _userManager.GetUserByUsername(request.Username);
            }

            if(user == null) 
            {
                return Result<UserDto>.Failure("User not found").WithCode(404);
            }

            return Result<UserDto>.Success(_userService.CreateUserDto(user));
        }
    }
}