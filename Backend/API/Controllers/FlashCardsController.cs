using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.FlashCards.Commands;
using Application.Features.FlashCards.Commands.Dto;
using Application.Features.FlashCards.Queries;
using Application.Features.FlashCards.Queries.Dto;
using Domain.Entities;
using Infrastructure.Security.Authorization;
using Langscape.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FlashCardsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public FlashCardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IResult<IReadOnlyList<GetFlashCardsSetDto>>>> GetCardsSets(CancellationToken cancellationToken, [FromQuery] int maximumNumberOfWords = 3)
        {
            return await _mediator.Send(new GetCardsSetsQuery() { MaximumNumberOfWords = maximumNumberOfWords}, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IResult<GetFlashCardsSetDto>>> GetCardsSet(CancellationToken cancellationToken, Guid id)
        {
            return await _mediator.Send(new GetCardsSetQuery { SetId = id }, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<IResult<string>>> CreateCardsSet(CancellationToken cancellationToken, FlashCardsSet set)
        {
            return await _mediator.Send(new CreateCardsSetCommand { FlashCardSet = set }, cancellationToken);
        }

        [Authorize(Policy = nameof(IsOwnerRequirement<FlashCardsSet>))]
        [HttpPut("{id}")]
        public async Task<ActionResult<IResult<Unit>>> EditCardsSet(CancellationToken cancellationToken, Guid id, EditFlashCardsSetDto set)
        {
            set.Id = id;
            return await _mediator.Send(new EditCardsSetCommand { FlashCardSet = set }, cancellationToken);
        }

        [Authorize(Policy = nameof(IsOwnerRequirement<FlashCardsSet>))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<IResult<Unit>>> DeleteCardsSet(CancellationToken cancellationToken, Guid id)
        {
            return await _mediator.Send(new DeleteCardsSetCommand { Id = id }, cancellationToken);
        }
    }
}