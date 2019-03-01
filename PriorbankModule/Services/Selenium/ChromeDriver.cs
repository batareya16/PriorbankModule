using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumChromeDriver = OpenQA.Selenium.Chrome.ChromeDriver;
using System.Drawing;

namespace PriorbankModule.Services.Selenium
{
    sealed class ChromeDriver : ISeleniumDriver
    {
        public IWebDriver InitializeSeleniumWebDriver(string binaryLocation)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            var options = new ChromeOptions() { BinaryLocation = binaryLocation };

            //--Headless do not use. It's may spawn recaptcha field
            options.AddArguments(
                "--silent-launch",
                "--window-position=-0,0",
                "--no-startup-window",
                "--log-level=3");

            var driver = new SeleniumChromeDriver(service, options);
            //driver.Manage().Window.Position = new Point(-2000, 0);
            return driver;
        }
    }
}
