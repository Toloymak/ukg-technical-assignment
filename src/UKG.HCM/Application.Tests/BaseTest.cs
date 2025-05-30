namespace UKG.HCM.Application.Tests;

public abstract class BaseUnitTest
{
    protected Fixture Fixture { get; }
    
    protected BaseUnitTest()
    {
        Fixture = new Fixture();
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}