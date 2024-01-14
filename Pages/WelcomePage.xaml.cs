using Microsoft.Maui.Controls;
using ServiceBors.DB;

namespace ServiceBors
{
    public partial class WelcomePage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editAppointmentId;

        public WelcomePage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
        }

        private async void OnAppointmentsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.AppointmentsPage(_dbService));
        }

        private async void OnServicesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.ServicesPage(_dbService));
        }
        private async void OnPartnersClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.PartnerPage(_dbService));
        }
    }
}
