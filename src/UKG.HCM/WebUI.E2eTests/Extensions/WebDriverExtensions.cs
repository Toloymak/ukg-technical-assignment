using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UKG.HCM.WebUI.E2eTests.Objects;

namespace UKG.HCM.WebUI.E2eTests.Extensions;

public static class WebDriverExtensions
{
    public static Input InputWithTestId(this IWebDriver webDriver, string testId)
        => Input.CreateForTestId(webDriver, testId);
    
    public static Button ButtonWithTestId(this IWebDriver webDriver, string testId)
        => Button.CreateForTestId(webDriver, testId);
    
    public static Header HeaderWithTestId(this IWebDriver webDriver, string testId)
        => Header.CreateForTestId(webDriver, testId);
    
    public static void WaitForElement(
        this IWebDriver webDriver,
        Func<IWebElement> element,
        TimeSpan? timeout = null)
    {
        var wait = new WebDriverWait(webDriver, timeout ?? TimeSpan.FromSeconds(10));
        wait.Until(_ => element());
    }
}