using DDDInPractice.Logic.Atms;
using DDDInPractice.Logic.SharedKernel;
using DDDInPractice.UI.Common;

namespace DDDInPractice.UI.Atms
{
    public class AtmViewModel : ViewModel
    {
        private readonly Atm _atm;
        private readonly AtmRepository _atmRepository;

        public override string Caption => "ATM";

        public Money MoneyInside => _atm.MoneyInside;
        public string MoneyCharged => _atm.MoneyCharged.ToString("C2");
        public Command<decimal> TakeMoneyCommand { get; private set; }

        private string _message = "";
        public string Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                Notify();
            }
        }

        public AtmViewModel(Atm atm)
        {
            _atm = atm;
            _atmRepository = new AtmRepository();
            TakeMoneyCommand = new Command<decimal>(x => x > 0, TakeMoney);
        }

        private void TakeMoney(decimal amount)
        {
            var error = _atm.CanTakeMoney(amount);

            if (!string.IsNullOrEmpty(error))
            {
                NotifyClient(error);
                return;
            }

            _atm.TakeMoney(amount);
            _atmRepository.Save(_atm);
            NotifyClient("You have taken " + amount.ToString("C2"));
        }

        private void NotifyClient(string message)
        {
            Message = message;
            Notify(nameof(MoneyInside));
            Notify(nameof(MoneyCharged));
        }
    }
}
