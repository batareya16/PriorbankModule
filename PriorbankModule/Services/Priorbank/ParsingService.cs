using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PriorbankModule.Services.Selenium;

namespace PriorbankModule.Services.Priorbank
{
    sealed class ParsingService : IParsingDataService
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private Configuration _configuration;

        public ParsingService(IWebDriver driver, ref Configuration configuration)
        {
            _driver = driver;
            _configuration = configuration;
            _wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
        }

        public List<string> ParseCardData()
        {
            LoginIntoAccount();
            WaitCardsList();
            SelectCard();
            GetCardHistory();
            WaitCardsHistory();
            return GetCardHistoryData();
        }

        private void LoginIntoAccount()
        {
            _driver.Url = "https://www.prior.by/web/";
            _driver.SendKeysInto(By.Name("UserName"), _configuration.Login);
            _driver.SendKeysInto(By.Name("Password"), _configuration.Password);
            _driver.FindElement(By.Name("Password")).Submit();
            _driver.Url = "https://www.prior.by";
        }

        private void WaitCardsList()
        {
            _wait.WaitForAnyElement(By.ClassName("panel-card-text"));
        }

        private void SelectCard()
        {
            _driver.ClickElement(
                _driver
                    .FindElements(By.ClassName("panel-cards-item"))
                    .Where(el => el
                        .FindElements(By.ClassName("panel-card-text"))
                        .FirstOrDefault(x => x.Text == _configuration.CardName) != null)
                    .FirstOrDefault());
        }

        private void WaitCardsHistory()
        {
            _wait.WaitForAnyElement(By.CssSelector(".vpsk-info-body .k-grid[data-role='grid']"));
        }

        private void GetCardHistory()
        {
            _driver.ClickElement(By.CssSelector("[data-link-action='history']"));
            _wait.WaitForAnyElement(By.CssSelector(".detailedreport-cards-filter [name='Periods.SelectedValue'][value='Period']"));
            _driver.ClickElement(By.CssSelector(".detailedreport-cards-filter [name='Periods.SelectedValue'][value='Period']"));
            FillReportDateInput("DateFrom", _configuration.LastUpdate);
            FillReportDateInput("DateTo", DateTime.Now);
            _driver.ClickElement(By.CssSelector("[name='btnFilterSubmit'][type=submit]"));
        }

        private void FillReportDateInput(string elementName, DateTime date)
        {
            var dateElement = _driver.FindElement(By.CssSelector("[name='" + elementName + "']"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value='" + date.ToString("dd.MM.yyyy") + "'", dateElement);
        }

        private List<string> GetCardHistoryData()
        {
            var gridsCount = _driver.ElementsCount(By.CssSelector(".vpsk-info-body .k-grid[data-role='grid']"));
            var results = new List<string>();
            for (int i = 0; i < gridsCount; i++)
            {
                results.Add((string)((IJavaScriptExecutor)_driver).ExecuteScript(
                    "return JSON.stringify(jQuery('.vpsk-info-body .k-grid[data-role=grid]').eq(arguments[0]).getKendoGrid().dataSource.data())", i));
            }
            return results;
        }
    }
}
