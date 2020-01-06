﻿using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.Management;
using DDDInPractice.Logic.SharedKernel;
using System;
using static DDDInPractice.Logic.SharedKernel.Money;

namespace DDDInPractice.Logic.Atms
{
    public class Atm : AggregateRoot
    {
        private const decimal CommissionRate = 0.01m;
        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual decimal MoneyCharged { get; protected set; }

        public virtual string CanTakeMoney(decimal amount)
        {
            if (amount <= 0)
                return "Invalid amount";

            if (MoneyInside.Amount < amount)
                return "Not enough money";

            if (!MoneyInside.CanAllocate(amount))
                return "Not enough change";

            return string.Empty;
        }

        public virtual void TakeMoney(decimal amount)
        {
            if (!string.IsNullOrEmpty(CanTakeMoney(amount)))
                throw new InvalidOperationException();

            var output = MoneyInside.Allocate(amount);
            MoneyInside -= output;

            var amountWithCommission = CalculateAmountWithCommission(amount);
            MoneyCharged += amountWithCommission;

            //DomainEvents.Raise(new BalanceChangedEvent(amountWithCommission)); // Disparando evento do valor para o domínio.

            AddDomainEvent(new BalanceChangedEvent(amountWithCommission));
        }

        public virtual void LoadMoney(Money money)
        {
            MoneyInside += money;
        }

        private decimal CalculateAmountWithCommission(decimal amount)
        {
            var commission = amount * CommissionRate;
            var lessThanCent = commission % 0.01m;

            if (lessThanCent > 0)
            {
                commission = commission - lessThanCent + 0.01m;
            }

            return amount + commission;
        }
    }
}
