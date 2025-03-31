using UKG.HCM.WebApi.Contracts;

namespace UKG.HCM.WebApi.Configuration.PolicyNames;

public static class Policies
{
    public static class People
    {
        public static readonly RoleBasedPolicy ReadAll = new()
        {
            Name = "People.ReadAll",
            Roles = [Roles.HrAdmin, Roles.Manager, Roles.Employee]
        };
        
        public static readonly RoleBasedPolicy Create = new()
        {
            Name = "People.Create",
            Roles = [Roles.HrAdmin, Roles.Manager]
        };
        
        public static readonly RoleBasedPolicy Update = new()
        {
            Name = "People.Update",
            Roles = [Roles.HrAdmin, Roles.Manager]
        };
        
        public static readonly RoleBasedPolicy Delete = new()
        {
            Name = "People.Delete",
            Roles = [Roles.HrAdmin, Roles.Manager]
        };
    }
    
}

public class RoleBasedPolicy
{
    public required string Name { get; init; }
    public required string[] Roles { get; init; }
}