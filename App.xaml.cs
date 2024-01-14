using ServiceBors.DB;
namespace ServiceBors
   

{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            LocalDbService dbService = new LocalDbService();

            MainPage = new NavigationPage(new WelcomePage(dbService));
        }
    }
}
