using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PriorbankModule.Entities;
using Newtonsoft.Json;
using AutoMapper;
using PriorbankModule.Common;
using PriorbankModule.Services.Selenium;
using PriorbankModule.Services.Priorbank;

namespace PriorbankModule
{
    public class Main
    {
        public string GetData(ref string config)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
            var configObj = Serializer.Deserialize<Configuration>(config);
            ISeleniumDriver driver = new ChromeDriver();
            IParsingDataService parsingService = new ParsingService(driver.InitializeSeleniumWebDriver(), ref configObj);
            IIncomeProcessor incomeProcessor = new IncomeProcessor(parsingService.ParseCardData(), ref configObj);
            var incomes = incomeProcessor.ProcessIncomes();
            config = Serializer.Serialize<Configuration>(configObj);
            return Serializer.Serialize<List<Income>>(incomes);
        }
    }
}
