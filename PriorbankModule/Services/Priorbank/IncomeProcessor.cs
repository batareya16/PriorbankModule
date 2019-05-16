using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriorbankModule.Entities;
using Newtonsoft.Json;
using PriorbankModule.Common;

namespace PriorbankModule.Services.Priorbank
{
    sealed class IncomeProcessor : IIncomeProcessor
    {
        private readonly PriorbankLockedTransaction[] _lockedTransactions;
        private readonly PriorbankTransaction[] _contractOperations;
        private readonly PriorbankTransaction[] _cardOperations;
        private Configuration _configuration;

        public IncomeProcessor(ParsedDataEntity parsedDataEntity, ref Configuration configuration)
        {
            var iterator = 0;
            _lockedTransactions = parsedDataEntity.ReceivedDataFlags.HasFlag(PriorbankReceivedData.LockedOperations)
                ? JsonConvert.DeserializeObject<PriorbankLockedTransaction[]>(parsedDataEntity.SerializedData[iterator++])
                : new PriorbankLockedTransaction[0];
            _contractOperations = parsedDataEntity.ReceivedDataFlags.HasFlag(PriorbankReceivedData.ContractOperations)
                ? JsonConvert.DeserializeObject<PriorbankTransaction[]>(parsedDataEntity.SerializedData[iterator++])
                : new PriorbankTransaction[0];
            _cardOperations = parsedDataEntity.ReceivedDataFlags.HasFlag(PriorbankReceivedData.CardOperations)
                ? JsonConvert.DeserializeObject<PriorbankTransaction[]>(parsedDataEntity.SerializedData[iterator++])
                : new PriorbankTransaction[0];
            _configuration = configuration;
        }

        public List<Income> ProcessIncomes()
        {
            IEnumerable<PriorbankTransaction> transactions = ExcludeDuplicatedTransactions(_cardOperations.Concat(_contractOperations));
            var incomes = ConversionHelper.Convert(transactions
                .Union(ProcessLockedTransactions().Select(x =>
                {
                    x.AccountAmountString = AmountHelper.ReverseAccountAmountString(x.AccountAmountString);
                    return x;
                }))
                .Where(x =>
                    x.TransDate.Date > _configuration.LastUpdate.Date ||
                    (x.TransDate.Date == _configuration.LastUpdate.Date
                        && x.TransTime.TimeOfDay >= _configuration.LastUpdate.TimeOfDay)));

            _configuration.LastUpdate = DateTime.Now;
            return incomes.ToList();
        }

        private IEnumerable<PriorbankTransaction> ExcludeDuplicatedTransactions(IEnumerable<PriorbankTransaction> receivedTransactions)
        {
            var result = receivedTransactions.Except(_configuration.LastGivenTransactions);
            _configuration.LastGivenTransactions = receivedTransactions.ToList();
            return result;
        }

        private IEnumerable<PriorbankTransaction> ProcessLockedTransactions()
        {
            var prevLockedTransactions = _configuration.LockedTransactions == null
                ? new List<PriorbankTransaction>()
                : _configuration.LockedTransactions.Except(_configuration.LastGivenTransactions).ToList();
            var currLockedTransactions = ConversionHelper.Convert(_lockedTransactions)
                .Except(prevLockedTransactions);
            _configuration.LockedTransactions = prevLockedTransactions.Union(currLockedTransactions).ToArray();
            return currLockedTransactions;
        }
    }
}
