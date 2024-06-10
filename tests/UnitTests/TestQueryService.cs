using Moq;
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
    public class TestQueryService
    {
        Mock<IPlayerRepository> _mock;
        IPlayerQueryService _service;

        public TestQueryService()
        {
            _mock=new Mock<IPlayerRepository>();
            _service=new PlayerQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Player>());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllPlayers());

            Assert.Equal(Constants.NO_PLAYERS_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetAll_ReturnPlayers()
        {

            var players = TestPlayerFactory.CreatePlayers(5);

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(players);

            var result = await _service.GetAllPlayers();

            Assert.NotNull(result);
            Assert.Contains(players[1], result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((Player)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

            Assert.Equal(Constants.PLAYER_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetByName_ReturnPlayer()
        {

            var player = TestPlayerFactory.CreatePlayer(1);

            player.Name = "test";

            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(player);

            var result = await _service.GetByName("test");

            Assert.NotNull(result);
            Assert.Equal(player, result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Player)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(Constants.PLAYER_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetById_ReturnPlayer()
        {
            var player = TestPlayerFactory.CreatePlayer(2);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(player);

            var result = await _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(player, result);
        }



    }
}
