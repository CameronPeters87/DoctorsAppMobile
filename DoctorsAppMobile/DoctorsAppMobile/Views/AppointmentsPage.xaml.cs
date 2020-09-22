using DoctorsAppMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorsAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentsPage : ContentPage
    {
        public AppointmentsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var appointment = new AppointmentViewModel();
            await appointment.Initialise();

            appointmentsListView.ItemsSource = appointment.AppointmentModels;
            appointmentsListView.Refreshing += AppointmentsListView_Refreshing;
        }

        private void AppointmentsListView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            appointmentsListView.IsRefreshing = false;
        }
    }
}