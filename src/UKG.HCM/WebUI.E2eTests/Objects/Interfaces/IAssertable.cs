namespace UKG.HCM.WebUI.E2eTests.Objects.Interfaces;

public interface IAssertable<out T>
{
    T Assert(Action<T> assert);
}