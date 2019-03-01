using System.Linq;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace PriorbankModule.Services.Selenium
{
    static class SeleniumHelper
    {
        public static void WaitForAnyElement(this WebDriverWait wait, By by)
        {
            wait.Until((x) => x.FindElements(by).Any());
        }

        public static void ClickElement(this IWebDriver driver, By by)
        {
            ClickElement(driver, driver.FindElement(by));
        }

        public static void ClickElement(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", element);
        }

        public static int ElementsCount(this IWebDriver driver, By by)
        {
            return driver.FindElements(by).Count;
        }

        public static void SendKeysInto(this IWebDriver driver, By by, string keys)
        {
            driver.FindElement(by).SendKeys(keys);
        }
    }
}
