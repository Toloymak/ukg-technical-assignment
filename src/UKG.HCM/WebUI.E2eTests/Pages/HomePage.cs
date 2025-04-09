using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UKG.HCM.WebUI.E2eTests.Extensions;
using UKG.HCM.WebUI.E2eTests.Objects;
using UKG.HCM.WebUI.E2eTests.Objects.Interfaces;

namespace UKG.HCM.WebUI.E2eTests.Pages;

public class HomePage : BaseComponent, IWaitable, IAssertable<HomePage>
{
    private readonly IWebDriver _driver;

    public HomePage(IWebDriver driver)
    {
        _driver = driver;
    }

    private Header WelcomeMessage => _driver.HeaderWithTestId("login");
    
    public bool IsDisplayedWelcome()
        => WelcomeMessage.IsDisplayed();

    public void Wait(TimeSpan? timeout = null)
        => WelcomeMessage.Wait();

    public HomePage Assert(Action<HomePage> assert)
    {
        assert.Invoke(this);
        return this;
    }
}