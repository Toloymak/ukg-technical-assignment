using UKG.HCM.WebUI.E2eTests.DataModels;

namespace UKG.HCM.WebUI.E2eTests.TestData;

// TODO: Use data from the config
public static class Users
{
    public static UserData Admin => new()
    {
        Login = "admin",
        Password = "Pass1234"
    };
    
    public static UserData Employee => new()
    {
        Login = "employee",
        Password = "Pass1234"
    };
    
    public static UserData Manager => new()
    {
        Login = "manager",
        Password = "Pass1234"
    };
}