using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PriorbankModule.Entities;
using Newtonsoft.Json;
using AutoMapper;
using PriorbankModule.Common;

namespace PriorbankModule
{
    public class Main
    {
        public string GetData(ref string config)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
            var configObj = Serializer.Deserialize<Configuration>(config);

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            options.BinaryLocation = "C:\\";
            //TODO: Remove useless & try to check what is nessessary for hiddden browser window
            options.AddArgument("--window-position=-2000,0");
            options.AddArgument("--no-startup-window");
            options.AddArgument("--log-level=3");
            //--Headless do not use! It's may spawn recaptcha field

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Manage().Window.Position = new System.Drawing.Point(-2000, 0);
            driver.Url = "https://www.prior.by/web/";
            var element = driver.FindElement(By.Name("UserName"));
            element.SendKeys("shagaldemo");
            element = driver.FindElement(By.Name("Password"));
            element.SendKeys("kovalevskayademo");
            element.Submit();
            driver.Url = "https://www.prior.by";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until((x) => x.FindElements(By.ClassName("panel-card-text")).Any());
            var card = driver
                .FindElements(By.ClassName("panel-cards-item"))
                .Where(el => el
                    .FindElements(By.ClassName("panel-card-text"))
                    .FirstOrDefault(x => x.Text == "DK6191 ") != null)
                .FirstOrDefault();
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", card);
            //card.Click();
            driver.FindElement(By.CssSelector("[data-link-action='history']")).Click();
            var filterBt = driver.FindElement(By.CssSelector(".detailedreport-cards-filter [name='Periods.SelectedValue'][value='Period']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", filterBt);
            // wait.Until((x) => x.FindElements(By.ClassName("panel-card-text")).Any());
            var dateFrom = driver.FindElement(By.CssSelector("[name='DateFrom']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value='" + DateTime.Now.AddDays(-5).ToString("dd.MM.yyyy") + "'", dateFrom);
            var dateTo = driver.FindElement(By.CssSelector("[name='DateTo']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value='" + DateTime.Now.ToString("dd.MM.yyyy") + "'", dateTo);
            //dateTo.Submit()
            driver.FindElement(By.CssSelector("[name='btnFilterSubmit'][type=submit]")).Click();
            
            wait.Until((x) => x.FindElements(By.CssSelector(".vpsk-info-body .k-grid[data-role='grid']")).Any());
            var gridsCount = driver.FindElements(By.CssSelector(".vpsk-info-body .k-grid[data-role='grid']")).Count;
            var results = new List<string>();
            for (int i = 0; i < gridsCount; i++)
            {
                results.Add((string)((IJavaScriptExecutor)driver).ExecuteScript(
                    "return JSON.stringify(jQuery('.vpsk-info-body .k-grid[data-role=grid]').eq(arguments[0]).getKendoGrid().dataSource.data())", i));
            }

            var lockedTransactions = JsonConvert.DeserializeObject<PriorbankLockedTransaction[]>(results[0]);
            var contractOperations = JsonConvert.DeserializeObject<PriorbankTransaction[]>(results[1]);
            var cardOperations = JsonConvert.DeserializeObject<PriorbankTransaction[]>(results[2]);

            configObj.LastUpdate = DateTime.Now.AddDays(-4);
            var incomes = Mapper.Map<Income[]>(cardOperations.Concat(contractOperations)
                .Where(x => x.TransDate.Date >= configObj.LastUpdate.Date && x.TransTime.TimeOfDay >= configObj.LastUpdate.TimeOfDay));

            //Парсим AccountAmountString и его снимаем
            //Придумать что делать с ReCaptcha
            
            //driver.FindElement(By.CssSelector("[name='DateTo']")).SendKeys(DateTime.Now.ToString("dd.MM.yyyy"));
            //driver.FindElement(By.CssSelector("[name='DateTo']")).Submit();

            //prior-user-welcome_wnd_title

            configObj.LastUpdate = DateTime.Now;
            config = Serializer.Serialize<Configuration>(configObj);
            return Serializer.Serialize<List<Income>>(incomes.ToList());
        }
    }
}
