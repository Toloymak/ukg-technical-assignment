namespace UKG.HCM.WebUI.E2eTests.Objects.Interfaces;

public interface IWaitable
{
    void Wait(TimeSpan? timeout = null);
}