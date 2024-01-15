using ServiceBors.DB;

namespace ServiceBors.Pages
{
    public partial class PartnerPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editPartnerId;

        public PartnerPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () =>
            {
                listView.ItemsSource = await _dbService.GetPartners();
                PopulateServicePicker();
            });
        }

        private async void PopulateServicePicker()
        {
            var services = await _dbService.GetServices();
            servicePicker.ItemsSource = services;
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (_editPartnerId == 0)
            {
                await _dbService.CreatePartner(new Partner
                {
                    PartnerName = partnerNameEntryField.Text,
                    PartnerLocation = partnerLocationEntryField.Text,
                    Service = servicePicker.SelectedItem as string,
                    PartnerType = partnerTypeEntryField.Text,
                });
            }
            else
            {
                await _dbService.UpdatePartner(new Partner
                {
                    Id = _editPartnerId,
                    PartnerName = partnerNameEntryField.Text,
                    PartnerLocation = partnerLocationEntryField.Text,
                    PartnerType = partnerTypeEntryField.Text,
                    Service = servicePicker.SelectedItem as string,

                });
                _editPartnerId = 0;
            }

            partnerNameEntryField.Text = string.Empty;
            partnerLocationEntryField.Text = string.Empty;
            partnerTypeEntryField.Text = string.Empty;
            servicePicker.SelectedIndex = -1;


            listView.ItemsSource = await _dbService.GetPartners();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var partner = (Partner)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editPartnerId = partner.Id;
                    partnerNameEntryField.Text = partner.PartnerName;
                    partnerLocationEntryField.Text = partner.PartnerLocation;
                    partnerTypeEntryField.Text = partner.PartnerType;
                    servicePicker.SelectedItem = partner.Service;

                    break;

                case "Delete":
                    await _dbService.DeletePartner(partner);
                    listView.ItemsSource = await _dbService.GetPartners();
                    break;
            }
        }
        private void locationButton_Clicked(object sender, EventArgs e)
        {
            var partner = _dbService.GetPartnerById(_editPartnerId).Result;

            if (partner != null && !string.IsNullOrWhiteSpace(partner.PartnerLocation))
            {
                var locationUrl = $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(partner.PartnerLocation)}";

                Launcher.OpenAsync(new Uri(locationUrl));
            }
            else
            {
                DisplayAlert("Error", "Partner location not available.", "OK");
            }
        }

    }
}
