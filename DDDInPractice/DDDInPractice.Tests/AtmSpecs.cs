using DDDInPractice.Logic.Atms;
using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.Management;
using DDDInPractice.Logic.SharedKernel;
using DDDInPractice.Logic.Utils;
using FluentAssertions;
using Xunit;

namespace DDDInPractice.Tests
{
    public class AtmSpecs
    {
        [Fact]
        public void Take_money_exchanges_money_with_commission()
        {
            var atm = new Atm();
            atm.LoadMoney(Money.Dollar);

            atm.TakeMoney(1m);

            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.01m);
        }

        [Fact]
        public void Commission_is_at_least_one_cent()
        {
            var atm = new Atm();
            atm.LoadMoney(Money.Cent);

            atm.TakeMoney(0.01m);

            atm.MoneyCharged.Should().Be(0.02m);
        }

        [Fact]
        public void Commission_is_rounded_up_to_the_next_cent()
        {
            var atm = new Atm();
            atm.LoadMoney(Money.Dollar + Money.TenCent);

            atm.TakeMoney(1.1m);
            atm.MoneyCharged.Should().Be(1.12m);
        }

        [Fact]
        public void Take_money_raises_an_event()
        {
            Initer.Init(@"Server=(localdb)\MSSqlLocalDB;Database=DDDInPractice;Trusted_Connection=true");

            var atm = new Atm();
            atm.LoadMoney(Money.Dollar);

            atm.TakeMoney(1m);

            var balanceChangedEvent = atm.DomainEvents[0] as BalanceChangedEvent;

            balanceChangedEvent.Should().NotBeNull();
            balanceChangedEvent.Delta.Should().Be(1.01m);
        }


    }
}
