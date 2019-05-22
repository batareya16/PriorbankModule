using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PriorbankModule.Services.Selenium;
using PriorbankModule.Entities;
using PriorbankModule.Common;

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

        public ParsedDataEntity ParseCardData()
        {
            OpenLoginPage();
            if (IsExistRecaptcha()) return new ParsedDataEntity();
            LoginIntoAccount();
            WaitCardsList();
            SelectCard();
            WaitCardsHistoryLink();
            GetCardHistory();
            WaitCardsHistory();
            return new ParsedDataEntity() { SerializedData = GetCardHistoryData(), ReceivedDataFlags = GetDataTypes() };
        }

        private void OpenLoginPage()
        {
            _driver.Url = "https://www.prior.by/web/";
        }

        private void LoginIntoAccount()
        {
            _driver.SendKeysInto(By.Name("UserName"), _configuration.Login);
            _driver.SendKeysInto(By.Name("Password"), _configuration.Password);
            _driver.FindElement(By.Name("Password")).Submit();
            _driver.Url = "https://www.prior.by";
        }

        private void SetInitialAmount()
        {
            if (_configuration.FirstLaunch)
            {
                AmountHelper.InitialBudgetAmount = AmountHelper.GetAccountAmount(
                    ((IJavaScriptExecutor)_driver).ExecuteScript(string.Format(
                        "return jQuery('.bank-cards-list.manager [data-card-name=\"{0}\"]').closest('tr').find('.total-amount .sum').html()",
                        _configuration.CardName)).ToString());
            }
        }

        private void WaitCardsList()
        {
            _wait.WaitForAnyElement(By.ClassName("panel-card-text"));
        }

        private bool IsExistRecaptcha()
        {
            return _driver.ElementsCount(By.ClassName("recaptcha-checkbox")) > 0;
        }

        private void SelectCard()
        {
            _driver.ClickElement(
                _driver
                    .FindElements(By.ClassName("panel-cards-item"))
                    .FirstOrDefault(el => el.FindElements(By.ClassName("panel-card-text"))
                                            .FirstOrDefault(x => x.Text.Trim() == _configuration.CardName) != null));
        }

        private void WaitCardsHistory()
        {
            _wait.WaitForAnyElement(By.CssSelector(".vpsk-info-body .k-grid[data-role='grid']"));
        }

        private void WaitCardsHistoryLink()
        {
            _wait.WaitForAnyElement(By.CssSelector("[data-link-action='history']"));
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

        private PriorbankReceivedData GetDataTypes()
        {
            return (_driver.IsExistElementByInnerText("Заблокированные суммы по карте") ? PriorbankReceivedData.LockedOperations : PriorbankReceivedData.None)
                 | (_driver.IsExistElementByInnerText("Операции по контракту") ? PriorbankReceivedData.ContractOperations : PriorbankReceivedData.None)
                 | (_driver.IsExistElementByInnerText("Операции по карте") ? PriorbankReceivedData.CardOperations : PriorbankReceivedData.None);
        }
    }
}
