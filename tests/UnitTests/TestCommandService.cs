using Moq;
using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;
using PlayerCrudApi.Players.Repository.interfaces;
using PlayerCrudApi.Players.Service;
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
    public class TestCommandService
    {
        Mock<IPlayerRepository> _mock;
        IPlayerCommandService _service;

        public TestCommandService()
        {
            _mock = new Mock<IPlayerRepository>();
            _service = new PlayerCommandService(_mock.Object);
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

            var player = TestPlayerFactory.CreatePlayer(1);

            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(player);

            var exception = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.CreatePlayer(create));

            Assert.Equal(Constants.PLAYER_ALREADY_EXIST, exception.Message);

        }

        [Fact]
        public async Task Create_ReturnPlayer()
        {

            var create = new CreatePlayerRequest
            {
                Name="test",
                Number=10,
                Value=24550
            };

            var player = TestPlayerFactory.CreatePlayer(1);

            player.Name=create.Name;
            player.Number=create.Number;
            player.Value=create.Value;

            _mock.Setup(repo => repo.CreatePlayer(It.IsAny<CreatePlayerRequest>())).ReturnsAsync(player);

            var result = await _service.CreatePlayer(create);

            Assert.NotNull(result);
            Assert.Equal(result, player);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdatePlayerRequest
            {
                Name="test",
                Number=0,
                Value=0
            };

            var player = TestPlayerFactory.CreatePlayer(2);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((PlayerDto)null);

            var idException = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdatePlayer(1, update));

            Assert.Equal(Constants.PLAYER_DOES_NOT_EXIST, idException.Message);

        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdatePlayerRequest
            {

                Name="test",
                Number=10,
                Value=34009
            };

            var player = TestPlayerFactory.CreatePlayer(1);

            player.Name = update.Name;
            player.Number = update.Number.Value;
            player.Value = update.Value.Value;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(player);
            _mock.Setup(repo => repo.UpdatePlayer(It.IsAny<int>(), It.IsAny<UpdatePlayerRequest>())).ReturnsAsync(player);

            var result = await _service.UpdatePlayer(5, update);

            Assert.NotNull(result);
            Assert.Equal(player, result);
        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.DeletePlayerById(It.IsAny<int>())).ReturnsAsync((PlayerDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeletePlayer(5));

            Assert.Equal(Constants.PLAYER_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var player = TestPlayerFactory.CreatePlayer(1);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(player);

            var result = await _service.DeletePlayer(1);

            Assert.NotNull(result);
            Assert.Equal(player, result);

        }


    }
}
