﻿using AutoMapper;
using Cards.DTOs.CardDtos;
using Cards.Extensions.Entity;
using Cards.Helpers.Identity;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cards.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public class CardsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;
        private readonly ILogger<CardsController> _logger;
        private readonly IDateTimeFactory _dateTimeFactory;

        public CardsController(
            ICardRepository cardRepository,
            IMapper mapper,
            ILogger<CardsController> logger,
            IDateTimeFactory dateTimeFactory)
        {
            _mapper = mapper;
            _logger = logger;
            _cardRepository = cardRepository;
            _dateTimeFactory = dateTimeFactory;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<CardToDisplayDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get([FromQuery] QueryParams queryParams, [FromQuery] CardFilters filters)
        {
            try
            {
                var response = await IdentityHelper.IsLoggedInUserActive(HttpContext);

                if (response.user == null) return response.result;

                var results = await _cardRepository.GetCards<CardToDisplayDto>(queryParams, filters,
                    response.isInAdminRole, response.user.Id);

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));

                return StatusCode(500);
            }
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CardToDisplayDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                var response = await IdentityHelper.IsLoggedInUserActive(HttpContext);

                if (response.user == null) return response.result;

                var card = await _cardRepository.GetSingleCardById<CardToDisplayDto>(id, response.isInAdminRole,
                    response.user.Id);

                if (card is null)
                    return NotFound("Card not found or you do not have access to this card");

                return Ok(card);
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));

                return StatusCode(500);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(CardToDisplayDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> Post([FromBody] CardToCreateDto cardDto)
        {
            try
            {
                var response = await IdentityHelper.IsLoggedInUserActive(HttpContext);

                if (response.user == null) return response.result;

                var card = _mapper.Map<Card>(cardDto);

                card.OwnerId = response.user.Id;

                _cardRepository.Add(card);

                await _cardRepository.CompleteAsync();

                return Ok(_mapper.Map<CardToDisplayDto>(card));
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));

                return StatusCode(500);
            }
        }


        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CardToDisplayDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put(Guid id, [FromBody] CardToEditDto cardDto)
        {
            try
            {
                var response = await IdentityHelper.IsLoggedInUserActive(HttpContext);

                if (response.user == null) return response.result;

                var card = await _cardRepository.GetSingleCardById<Card>(id, response.isInAdminRole, response.user.Id);

                if (card == null) return NotFound("Card not found or you do not have access to this card");

                card.Update(cardDto, response.user.UserName!, _dateTimeFactory.Now());

                await _cardRepository.CompleteAsync();

                return Ok(_mapper.Map<CardToDisplayDto>(card));
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));

                return StatusCode(500);
            }
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CardToDisplayDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> Patch(Guid id, [FromBody] CardToPatchDto cardDto)
        {
            try
            {
                var response = await IdentityHelper.IsLoggedInUserActive(HttpContext);

                if (response.user == null) return response.result;

                var card = await _cardRepository.GetSingleCardById<Card>(id, response.isInAdminRole, response.user.Id);

                if (card == null) return NotFound("Card not found or you do not have access to this card");

                card.Patch(cardDto, response.user.UserName!, _dateTimeFactory.Now());

                await _cardRepository.CompleteAsync();

                return Ok(_mapper.Map<CardToDisplayDto>(card));
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));

                return StatusCode(500);
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var response = await IdentityHelper.IsLoggedInUserActive(HttpContext);

                if (response.user == null) return response.result;

                var card = await _cardRepository.GetSingleCardById<Card>(id, response.isInAdminRole, response.user.Id);

                if (card is not null)
                {
                    _cardRepository.Remove(card);

                    _cardRepository.Complete();

                    return Ok("Card deleted successfully");
                }

                return NotFound("Card not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));

                return StatusCode(500);
            }
        }
    }
}