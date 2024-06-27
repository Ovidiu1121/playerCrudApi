using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlayerCrudApi.Dto;
using PlayerCrudApi.Players.Model;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class PlayerIntegrationTests: IClassFixture<ApiWebApplicationFactory>
{
    
    private readonly HttpClient _client;

    public PlayerIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Player/create";
        var player = new CreatePlayerRequest() { Name = "new name", Number = 32, Value = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PlayerDto>(responseString);

        Assert.NotNull(result);
        Assert.Equal(player.Name, result.Name);
        Assert.Equal(player.Number, result.Number);
        Assert.Equal(player.Value, result.Value);
    }
    
    [Fact]
    public async Task Post_Create_PlayerAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/Player/create";
        var player = new CreatePlayerRequest() { Name = "new name", Number = 32, Value = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Player/create";
        var player = new CreatePlayerRequest() { Name = "new player", Number = 32, Value = 20};
        var content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PlayerDto>(responseString)!;

        request = "/api/v1/Player/update/"+result.Id;
        var updatePlayer = new UpdatePlayerRequest() { Name = "updated player", Number = 32, Value = 20 };
        content = new StringContent(JsonConvert.SerializeObject(updatePlayer), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<PlayerDto>(responseString)!;

        Assert.Equal(updatePlayer.Name, result.Name);
        Assert.Equal(updatePlayer.Number, result.Number);
        Assert.Equal(updatePlayer.Value, result.Value);
    }
    
    [Fact]
    public async Task Put_Update_PlayerDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Player/update/1";
        var updatePlayer = new UpdatePlayerRequest() { Name = "new name", Number = 32, Value = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(updatePlayer), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Delete_Delete_PlayerExists_ReturnsDeletedPlayer()
    {

        var request = "/api/v1/Player/create";
        var player = new CreatePlayerRequest() { Name = "new name", Number = 32, Value = 20  };
        var content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PlayerDto>(responseString)!;

        request = "/api/v1/Player/delete/" + result.Id;

        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_Delete_PlayerDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Player/delete/14";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Get_GetByName_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Player/create";
        var player = new CreatePlayerRequest() { Name = "new name", Number = 32, Value = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PlayerDto>(responseString)!;

        request = "/api/v1/Player/name/" + result.Name;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    [Fact]
    public async Task Get_GetByName_PlayerDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Player/name/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    [Fact]
    public async Task Get_GetById_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Player/create";
        var player = new CreatePlayerRequest() { Name = "new name", Number = 32, Value = 20 };
        var content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PlayerDto>(responseString)!;

        request = "/api/v1/Player/id/" + result.Id;

        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    [Fact]
    public async Task Get_GetById_PlayerDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Player/id/88";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    
}