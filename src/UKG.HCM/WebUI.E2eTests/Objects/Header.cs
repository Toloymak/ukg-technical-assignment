using OpenQA.Selenium;
using UKG.HCM.WebUI.E2eTests.Extensions;
using UKG.HCM.WebUI.E2eTests.Objects.Interfaces;

namespace UKG.HCM.WebUI.E2eTests.Objects;

public class Header(IWebDriver webDriver, By selector) : IWaitable
{
    private IWebElement Element => webDriver.FindElement(selector);
    
    public bool IsDisplayed()
        => Element.Displayed;

    public static Header CreateForTestId(IWebDriver webDriver, string testId)
        => new(webDriver, ByTestId(testId));

    public void Wait(TimeSpan? timeout = null)
        => webDriver.WaitForElement(() => Element, timeout);
}