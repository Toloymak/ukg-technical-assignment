using OpenQA.Selenium;

namespace UKG.HCM.WebUI.E2eTests.Objects;

public class Button(IWebDriver webDriver, By selector)
{
    private IWebElement Element => webDriver.FindElement(selector);
    
    public void Click()
    {
        Element.Click();
    }
    
    public static Button CreateForTestId(IWebDriver webDriver, string testId)
        => new(webDriver, ByTestId(testId));
}