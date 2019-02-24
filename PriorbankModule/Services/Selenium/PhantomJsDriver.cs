using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace PriorbankModule.Services.Selenium
{
    sealed class PhantomJsDriver : ISeleniumDriver
    {
        public IWebDriver InitializeSeleniumWebDriver(string binaryLocation)
        {
            var service = PhantomJSDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            var driver = new PhantomJSDriver(service);
            return driver;
        }
    }
}
