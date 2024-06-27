namespace PlayerCrudApi.Dto;

public class ListPlayerDto
{
    public ListPlayerDto()
    {
        playerList = new List<PlayerDto>();
    }
    
    public List<PlayerDto> playerList { get; set; }
}