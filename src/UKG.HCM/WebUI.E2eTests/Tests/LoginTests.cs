using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UKG.HCM.WebUI.E2eTests.Pages;
using UKG.HCM.WebUI.E2eTests.TestData;

namespace UKG.HCM.WebUI.E2eTests.Tests;

// TODO: Just a draft, need a base class for configuring the driver
[TestFixture]
public class LoginTests : IDisposable
{
    private IWebDriver? _driver;

    [SetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    [Test]
    public void Login_AsAdmin_ShouldSucceed()
    {
        if (_driver == null)
            throw new InvalidOperationException("Driver is not initialized.");
        
        var admin = Users.Admin;
        
        new NavigationHub(_driver)
            .GoToLoginPage()
            .Login(admin.Login, admin.Password)
            .Assert(page => page.IsDisplayedWelcome().Should().BeTrue());
    }

    public void Dispose()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }
}