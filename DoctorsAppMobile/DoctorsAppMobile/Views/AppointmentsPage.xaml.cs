using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Logic;
using DoctorsAppMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorsAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentsPage : ContentPage
    {
        List<AppointmentModel> availableAppointments;
        List<AppointmentModel> allAppointments;

        public AppointmentsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var appointmentLogic = new AppointmentLogic();
                await appointmentLogic.Init();

                allAppointments = appointmentLogic.AllAppointments;
                availableAppointments = appointmentLogic.AvailableAppointments;
            }
            catch (Exception e)
            {
                throw e;
            }

            General.AllAppointments = allAppointments;
            General.AvailableAppointments = availableAppointments;

            appointmentsListView.ItemsSource = availableAppointments;
            appointmentsListView.Refreshing += AppointmentsListView_Refreshing;
            appointmentsListView.ItemSelected += AppointmentsListView_ItemSelected;
        }

        private async void AppointmentsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var appointment = (AppointmentModel)e.SelectedItem;

            var patient = await PatientLogic.GetPatientAsync(General.UserId);
            var fullname = PatientLogic.GetFullName(patient.FirstName, patient.Surname);

            if (allAppointments.Any(a => a.PatientName == fullname))
            {
                await DisplayAlert("Appointment", "You already have an appointment booked!", "Okay");
            }
            else
            {
                var symptoms = await DisplayPromptAsync("Book Appointment",
                        string.Format("{0}, {1}", appointment.AppointmentTime.ToLongDateString(),
                            appointment.AppointmentTime.ToShortTimeString()),
                        "Book", "Close", "Your symptoms ...");

                if (string.IsNullOrEmpty(symptoms))
                {

                }
                else
                {
                    appointment.symtoms = symptoms;
                    appointment.diagnosis = string.Empty;
                    appointment.PatientModel = patient;
                    appointment.PatientName = string.Format("{0} {1}", patient.FirstName, patient.Surname);

                    await AppointmentLogic.UpdateCustomerAsync(appointment);

                }
            }
        }

        private void AppointmentsListView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            appointmentsListView.IsRefreshing = false;
        }
    }
}