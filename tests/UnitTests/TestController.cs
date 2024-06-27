using Microsoft.AspNetCore.Mvc;
using Moq;
using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Controller;
using PlayerCrudApi.Players.Controller.interfaces;
using PlayerCrudApi.Players.Model;
using PlayerCrudApi.Players.Service.interfaces;
using PlayerCrudApi.System.Constant;
using PlayerCrudApi.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests
{
    public class TestController
    {

        Mock<IPlayerCommandService> _command;
        Mock<IPlayerQueryService> _query;
        PlayerApiController _controller;

        public TestController()
        {

            _command = new Mock<IPlayerCommandService>();
            _query = new Mock<IPlayerQueryService>();
            _controller = new PlayerController(_command.Object, _query.Object);

        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _query.Setup(repo => repo.GetAllPlayers()).ThrowsAsync(new ItemDoesNotExist(Constants.PLAYER_DOES_NOT_EXIST));

            var result = await _controller.GetAll();
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.PLAYER_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {

            var players = TestPlayerFactory.CreatePlayers(5);

            _query.Setup(repo => repo.GetAllPlayers()).ReturnsAsync(players);

            var result = await _controller.GetAll();
            var okresult = Assert.IsType<OkObjectResult>(result.Result);
            var playersAll = Assert.IsType<ListPlayerDto>(okresult.Value);

            Assert.Equal(5, playersAll.playerList.Count);
            Assert.Equal(200, okresult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidData()
        {
            var create = new CreatePlayerRequest
            {
                Name="test",
                Number=0,
                Value=0
            };

            _command.Setup(repo => repo.CreatePlayer(It.IsAny<CreatePlayerRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.PLAYER_ALREADY_EXIST));

            var result = await _controller.CreatePlayer(create);
            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, bad.StatusCode);
            Assert.Equal(Constants.PLAYER_ALREADY_EXIST, bad.Value);
        }

        [Fact]
        public async Task Create_ValidData()
        {

            var create = new CreatePlayerRequest
            {
                Name="test",
                Number=40,
                Value=34440
            };

            var player = TestPlayerFactory.CreatePlayer(1);

            player.Name=create.Name;
            player.Number=create.Number;
            player.Value=create.Value;

            _command.Setup(repo => repo.CreatePlayer(create)).ReturnsAsync(player);

            var result = await _controller.CreatePlayer(create);
            var okResult = Assert.IsType<CreatedResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 201);
            Assert.Equal(player, okResult.Value);

        }

        [Fact]
        public async Task Update_InvalidDate()
        {

            var update = new UpdatePlayerRequest
            {
               Name="test",
               Number=0,
               Value=0
            };

            _command.Setup(repo => repo.UpdatePlayer(1, update)).ThrowsAsync(new ItemDoesNotExist(Constants.PLAYER_DOES_NOT_EXIST));

            var result = await _controller.UpdatePlayer(1, update);
            var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 404);
            Assert.Equal(bad.Value, Constants.PLAYER_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Update_ValidData()
        {

            var update = new UpdatePlayerRequest
            {
                Name="test",
                Number=50,
                Value=90200
            };

            var player = TestPlayerFactory.CreatePlayer(1);

            player.Name=update.Name;
            player.Number=update.Number.Value;
            player.Value=update.Value.Value;

            _command.Setup(repo => repo.UpdatePlayer(1, update)).ReturnsAsync(player);

            var result = await _controller.UpdatePlayer(1, update);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, player);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _command.Setup(repo => repo.DeletePlayer(1)).ThrowsAsync(new ItemDoesNotExist(Constants.PLAYER_DOES_NOT_EXIST));

            var result = await _controller.DeletePlayer(1);
            var notfound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notfound.StatusCode, 404);
            Assert.Equal(notfound.Value, Constants.PLAYER_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var player = TestPlayerFactory.CreatePlayer(1);

            _command.Setup(repo => repo.DeletePlayer(1)).ReturnsAsync(player);

            var result = await _controller.DeletePlayer(1);
            var okResult = Assert.IsType<AcceptedResult>(result.Result);

            Assert.Equal(202, okResult.StatusCode);
            Assert.Equal(player, okResult.Value);
        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _query.Setup(repo => repo.GetByName("")).ThrowsAsync(new ItemDoesNotExist(Constants.PLAYER_DOES_NOT_EXIST));

            var result = await _controller.GetByNameRoute("");
            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.PLAYER_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetByName_ReturnPlayer()
        {

            var player = TestPlayerFactory.CreatePlayer(1);

            player.Name="test";

            _query.Setup(repo => repo.GetByName("test")).ReturnsAsync(player);

            var result = await _controller.GetByNameRoute("test");
            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);

        }

    }
}
