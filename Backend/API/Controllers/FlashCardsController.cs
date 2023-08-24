using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.FlashCards.Commands;
using Application.Features.FlashCards.Queries;
using Application.Features.FlashCards.Queries.Dto;
using Domain.Entities;
using Langscape.Shared;
using MediatR;
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
        public async Task<ActionResult<IResult<IReadOnlyList<FlashCardSetDto>>>> GetFlashCards(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFlashCardsQuery(), cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IResult<FlashCardSet>>> GetFlashCard(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFlashCardQuery { SetId = id }, cancellationToken);
        }
    }
}