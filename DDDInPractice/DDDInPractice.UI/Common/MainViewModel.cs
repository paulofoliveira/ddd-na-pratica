using DDDInPractice.Logic;
using DDDInPractice.Logic.SnackMachines;
using DDDInPractice.Logic.Utils;
using DDDInPractice.UI.SnackMachines;

namespace DDDInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            Initer.Init(@"Server=(localdb)\MSSqlLocalDB;Database=DDDInPractice;Trusted_Connection=true");

            var repository = new SnackMachineRepository();

            var snackMachine = repository.GetById(1L);

            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
        }
    }
}
