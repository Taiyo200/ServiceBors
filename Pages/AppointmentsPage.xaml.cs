using ServiceBors.DB;

namespace ServiceBors.Pages;

public partial class AppointmentsPage : ContentPage
{
	private readonly LocalDbService _dbService;
	private int _editAppointmentId;
	public AppointmentsPage(LocalDbService dbService)
	{
		InitializeComponent();
        _dbService = dbService;
        Task.Run(async () => listView.ItemsSource = await _dbService.GetAppointments());
    }

    private async void saveButton_Clicked(object sender, EventArgs e)
    {
        if (_editAppointmentId == 0)
        {
            await _dbService.Create(new Appointment
            {
                Name = nameEntryField.Text,
                CarPlate = carplateEntryField.Text,
                CarModel = carmodelEntryField.Text,
                Service = serviceEntryField.Text,
                Issue = issuesEntryField.Text,
            });
        }
        else
        {
            await _dbService.Update(new Appointment
            {
                Id = _editAppointmentId,
                Name = nameEntryField.Text,
                CarPlate = carplateEntryField.Text,
                CarModel = carmodelEntryField.Text,
                Service = serviceEntryField.Text,
                Issue = issuesEntryField.Text,
            });
            _editAppointmentId = 0;

        }
        nameEntryField.Text = string.Empty;
        carplateEntryField.Text = string.Empty;
        carmodelEntryField.Text = string.Empty;
        serviceEntryField.Text = string.Empty;
        issuesEntryField.Text = string.Empty;

        listView.ItemsSource = await _dbService.GetAppointments();
    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var appointment = (Appointment)e.Item;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
        switch (action)
        {
            case "Edit":

                _editAppointmentId = appointment.Id;
                nameEntryField.Text = appointment.Name;
                carplateEntryField.Text = appointment.CarPlate;
                carmodelEntryField.Text = appointment.CarModel;
                serviceEntryField.Text = appointment.Service;
                issuesEntryField.Text = appointment.Issue;
                break;

            case "Delete":

                await _dbService.Delete(appointment);
                listView.ItemsSource = await _dbService.GetAppointments();

                break;
           
        }
    }
}