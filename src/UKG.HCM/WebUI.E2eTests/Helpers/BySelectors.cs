using OpenQA.Selenium;

namespace UKG.HCM.WebUI.E2eTests.Helpers;

public static class BySelectors
{
    public static By ByTestId(string testId)
        => By.CssSelector($"[data-testid='{testId}']");
}