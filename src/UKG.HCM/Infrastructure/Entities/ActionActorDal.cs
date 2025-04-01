namespace UKG.HCM.Infrastructure.Entities;

/// ActionActor. Someone who did action in the system. It might be person, system or service
public class ActionActorDal
{
    public Guid ActorId { get; init; }
    
    public Guid? PersonId { get; init; }
    public PersonDal? Person { get; init; }
    
    public string? Service { get; init; }

    
    public ActionActorDal(Guid personId) => PersonId = personId;

    public ActionActorDal(string service) => Service = service;

    internal ActionActorDal() { }
}