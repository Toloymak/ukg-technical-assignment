using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UKG.HCM.WebUI.E2eTests.Extensions;
using UKG.HCM.WebUI.E2eTests.Objects;
using UKG.HCM.WebUI.E2eTests.Objects.Interfaces;

namespace UKG.HCM.WebUI.E2eTests.Pages;

public class LoginPage : BaseComponent, IWaitable, IAssertable<LoginPage>
{
    private readonly IWebDriver _driver;

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    private Input LoginInput => _driver.InputWithTestId("login");
    private Input PasswordInput => _driver.InputWithTestId("pass");
    private Button SubmitButton => _driver.ButtonWithTestId("login-btn");
    
    public HomePage Login(string login, string password)
    {
        LoginInput.Fill(login);
        PasswordInput.Fill(password);
        SubmitButton.Click();
        
        var homePage = new HomePage(_driver);
        homePage.Wait();

        return homePage;
    }

    public void Wait(TimeSpan? timeout = null)
        => LoginInput.Wait();

    public LoginPage Assert(Action<LoginPage> assert)
    {
        assert.Invoke(this);
        return this;
    }
}