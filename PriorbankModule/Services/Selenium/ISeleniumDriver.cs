using OpenQA.Selenium;

namespace PriorbankModule.Services.Selenium
{
    public interface ISeleniumDriver
    {
        IWebDriver InitializeSeleniumWebDriver(string binaryLocation);
    }
}
