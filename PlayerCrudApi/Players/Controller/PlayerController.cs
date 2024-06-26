﻿using Microsoft.AspNetCore.Mvc;
using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Controller.interfaces;
using PlayerCrudApi.Players.Model;
using PlayerCrudApi.Players.Service.interfaces;
using PlayerCrudApi.System.Exceptions;

namespace PlayerCrudApi.Players.Controller
{
    public class PlayerController:PlayerApiController
    {
        private IPlayerCommandService _playerCommandService;
        private IPlayerQueryService _playerQueryService;

        public PlayerController(IPlayerCommandService playerCommandService, IPlayerQueryService playerQueryService)
        {
            _playerCommandService = playerCommandService;
            _playerQueryService = playerQueryService;
        }

        public override async Task<ActionResult<PlayerDto>> CreatePlayer([FromBody] CreatePlayerRequest request)
        {
            try
            {
                var players = await _playerCommandService.CreatePlayer(request);

                return Created("Player-ul a fost creat", players);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<PlayerDto>> DeletePlayer([FromRoute] int id)
        {
            try
            {
                var players = await _playerCommandService.DeletePlayer(id);

                return Accepted("", players);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ListPlayerDto>> GetAll()
        {
            try
            {
                var players = await _playerQueryService.GetAllPlayers();
                return Ok(players);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<PlayerDto>> GetByNameRoute([FromRoute] string name)
        {
            try
            {
                var players = await _playerQueryService.GetByName(name);
                return Ok(players);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<PlayerDto>> GetByIdRoute(int id)
        {
            try
            {
                var players = await _playerQueryService.GetById(id);
                return Ok(players);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<PlayerDto>> UpdatePlayer([FromRoute] int id, [FromBody] UpdatePlayerRequest request)
        {
            try
            {
                var players = await _playerCommandService.UpdatePlayer(id, request);

                return Ok(players);
            }
            catch (InvalidValue ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
