namespace UKG.HCM.WebApi.Configuration.PolicyNames;

public static class Policies
{
    public static class People
    {
        public static RoleBasedPolicy ReadAll = new()
        {
            Name = "People.ReadAll",
            Roles = new[] { "Admin", "HR" }
        };
    }
    
}

public class RoleBasedPolicy
{
    public string Name { get; init; }
    public string[] Roles { get; init; }
}