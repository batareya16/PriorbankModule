using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriorbankModule.Entities;
using Newtonsoft.Json;
using AutoMapper;
using PriorbankModule.Common;

namespace PriorbankModule.Services.Priorbank
{
    sealed class IncomeProcessor : IIncomeProcessor
    {
        private readonly PriorbankLockedTransaction[] _lockedTransactions;
        private readonly PriorbankTransaction[] _contractOperations;
        private readonly PriorbankTransaction[] _cardOperations;
        private Configuration _configuration;

        public IncomeProcessor(List<string> serializedCardHistory, ref Configuration configuration)
        {
            _lockedTransactions = JsonConvert.DeserializeObject<PriorbankLockedTransaction[]>(serializedCardHistory[0]);
            _contractOperations = JsonConvert.DeserializeObject<PriorbankTransaction[]>(serializedCardHistory[1]);
            _cardOperations = JsonConvert.DeserializeObject<PriorbankTransaction[]>(serializedCardHistory[2]);
            _configuration = configuration;
        }

        public List<Income> ProcessIncomes()
        {
            var incomes = Mapper.Map<Income[]>(_cardOperations.Concat(_contractOperations)
                .Where(x =>
                    x.TransDate.Date >= _configuration.LastUpdate.Date &&
                    x.TransTime.TimeOfDay >= _configuration.LastUpdate.TimeOfDay));

            _configuration.LastUpdate = DateTime.Now;
            return incomes.ToList();
        }
    }
}
