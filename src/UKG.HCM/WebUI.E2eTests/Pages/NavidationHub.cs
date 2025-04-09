using OpenQA.Selenium;

namespace UKG.HCM.WebUI.E2eTests.Pages;

public class NavigationHub(IWebDriver driver)
{
    public LoginPage GoToLoginPage()
    {
        // TODO: use URL from config
        driver.Navigate().GoToUrl("http://localhost:5361/login");
        var page = new LoginPage(driver);
        page.Wait();
        return page;
    }
}