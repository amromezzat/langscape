using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence.Repositories;

namespace Infrastructure.Security.Authorization
{
    public class IsOwnerRequirement<T> : IAuthorizationRequirement where T: class, IAuditableEntity
    {
    }

    public class IsOwnerRequirementHandler<T> : AuthorizationHandler<IsOwnerRequirement<T>> where T: class, IAuditableEntity
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public IsOwnerRequirementHandler(
            IHttpContextAccessor httpContext, 
            IUserAccessor userAccessor,
            IUnitOfWork unitOfWork)
        {
            _httpContext = httpContext;
            _userAccessor = userAccessor;
            _unitOfWork = unitOfWork;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerRequirement<T> requirement)
        {
            var userId = _userAccessor.GetUserId();

            if (userId == null) 
            {
                return;
            }

            var entityId = Guid.Parse(_httpContext.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var entity = await _unitOfWork.GetRepository<T>().GetByIdAsync(entityId);
            bool isOwner = entity?.CreatedById == userId;

            if (isOwner)
            {
                context.Succeed(requirement);
            }
        }
    }
}