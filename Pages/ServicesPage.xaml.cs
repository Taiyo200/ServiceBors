using ServiceBors.DB;

namespace ServiceBors.Pages
{
    public partial class ServicesPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editServiceId;

        public ServicesPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listView.ItemsSource = await _dbService.GetServices());
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (_editServiceId == 0)
            {
                await _dbService.CreateService(new Service
                {
                    ServiceName = serviceNameEntryField.Text,
                    ServiceDescription = serviceDescriptionEntryField.Text,
                    ServicePrice = Convert.ToDecimal(servicePriceEntryField.Text),
                });
            }
            else
            {
                await _dbService.UpdateService(new Service
                {
                    Id = _editServiceId,
                    ServiceName = serviceNameEntryField.Text,
                    ServiceDescription = serviceDescriptionEntryField.Text,
                    ServicePrice = Convert.ToDecimal(servicePriceEntryField.Text),
                });
                _editServiceId = 0;
            }

            serviceNameEntryField.Text = string.Empty;
            serviceDescriptionEntryField.Text = string.Empty;
            servicePriceEntryField.Text = string.Empty;

            listView.ItemsSource = await _dbService.GetServices();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var service = (Service)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editServiceId = service.Id;
                    serviceNameEntryField.Text = service.ServiceName;
                    serviceDescriptionEntryField.Text = service.ServiceDescription;
                    servicePriceEntryField.Text = service.ServicePrice.ToString();
                    break;

                case "Delete":
                    await _dbService.DeleteService(service);
                    listView.ItemsSource = await _dbService.GetServices();
                    break;
            }
        }
    }
}