namespace UKG.HCM.WebUI.E2eTests.DataModels;

public record UserData
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}