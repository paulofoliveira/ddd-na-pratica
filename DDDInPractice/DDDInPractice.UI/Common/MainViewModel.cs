using DDDInPractice.Logic;

namespace DDDInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            Initer.Init(@"Server=(localdb)\MSSqlLocalDB;Database=DDDInPractice;Trusted_Connection=true");

            SnackMachine snackMachine;

            using (var session = SessionFactory.OpenSession())
            {
                snackMachine = session.Get<SnackMachine>(1L);
            }

            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
        }
    }
}
