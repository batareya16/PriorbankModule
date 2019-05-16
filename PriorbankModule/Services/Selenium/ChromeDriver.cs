using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumChromeDriver = OpenQA.Selenium.Chrome.ChromeDriver;
using System.Drawing;
using System.IO;

namespace PriorbankModule.Services.Selenium
{
    public sealed class ChromeDriver : ISeleniumDriver
    {
        public const string ChromeDriverExeFileName = "chromedriver.exe";

        public IWebDriver InitializeSeleniumWebDriver()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            var options = new ChromeOptions() { BinaryLocation = Path.GetDirectoryName(GetChromeDriverExePath()) };

            options.AddArguments(
                "--headless",
                "--silent-launch",
                "--disable-plugins-discovery",
                "--incognito",
                "--window-position=-2000,0",
                "--no-startup-window",
                "--disable-user-media-security");

            var driver = new SeleniumChromeDriver(service, options);
            driver.Manage().Window.Position = new Point(-2000, 0);
            return driver;
        }

        private string GetChromeDriverExePath()
        {
            return Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                ChromeDriverExeFileName);
        }
    }
}
