using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using static DDDInPractice.Logic.SharedKernel.Money;

namespace DDDInPractice.Logic.SnackMachines
{
    public class SnackMachine : AggregateRoot
    {
        public virtual Money MoneyInside { get; protected set; }
        public virtual decimal MoneyInTransaction { get; protected set; }
        protected virtual IList<Slot> Slots { get; set; }

        public SnackMachine()
        {
            MoneyInside = None;
            MoneyInTransaction = 0;

            Slots = new List<Slot>()
            {
                new Slot(this, 1),
                new Slot(this, 2),
                new Slot(this, 3)
            };
        }

        public virtual void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };

            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money.Amount;
            MoneyInside += money;
        }

        public virtual void ReturnMoney()
        {
            var moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
            MoneyInside -= moneyToReturn;

            MoneyInTransaction = 0;
        }

        public virtual string CanBuySnack(int position)
        {
            var snackPile = GetSnackPile(position);

            if (snackPile.Quantity == 0)
                return "The snack pile is empty";

            if (MoneyInTransaction < snackPile.Price)
                return "Not enough money";

            if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
                return "Not enough change";

            return string.Empty;
        }

        public virtual void BuySnack(int position)
        {
            if (!string.IsNullOrEmpty(CanBuySnack(position)))
                throw new InvalidOperationException();

            var slot = GetSlot(position);

            slot.SnackPile = slot.SnackPile.SubtractOne();

            var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);

            MoneyInside -= change;

            MoneyInTransaction = 0;
        }

        public virtual void LoadSnacks(int position, SnackPile snackPile)
        {
            var slot = GetSlot(position);
            slot.LoadSnack(snackPile);
        }

        public virtual SnackPile GetSnackPile(int position)
        {
            var slot = GetSlot(position);
            return slot.SnackPile;
        }

        private Slot GetSlot(int position) => Slots.FirstOrDefault(p => p.Position == position);

        public virtual void LoadMoney(Money money)
        {
            MoneyInside += money;
        }

        public virtual IReadOnlyList<SnackPile> GetAllSnackPiles()
        {
            return Slots.OrderBy(p => p.Position)
                         .Select(p => p.SnackPile)
                         .ToList();
        }
    }
}
