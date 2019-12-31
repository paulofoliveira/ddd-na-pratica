using DDDInPractice.Logic;

namespace DDDInPractice.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            Initer.Init(@"Server=(localdb)\MSSqlLocalDB;Database=DDDInPractice;Trusted_Connection=true");
        }
    }
}
