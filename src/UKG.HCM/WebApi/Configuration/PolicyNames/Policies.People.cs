namespace UKG.HCM.WebApi.Configuration.PolicyNames;

public static class Policies
{
    public static class People
    {
        public static readonly RoleBasedPolicy ReadAll = new()
        {
            Name = "People.ReadAll",
            Roles = ["Admin", "HR"]
        };
        
        public static readonly RoleBasedPolicy Create = new()
        {
            Name = "People.Create",
            Roles = ["Admin", "HR"]
        };
        
        public static readonly RoleBasedPolicy Update = new()
        {
            Name = "People.Update",
            Roles = ["Admin", "HR"]
        };
        
        public static readonly RoleBasedPolicy Delete = new()
        {
            Name = "People.Delete",
            Roles = ["Admin", "HR"]
        };
    }
    
}

public class RoleBasedPolicy
{
    public required string Name { get; init; }
    public required string[] Roles { get; init; }
}