﻿using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Logic;
using DoctorsAppMobile.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorsAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentAppointmentPage : ContentPage
    {
        AppointmentModel current;
        public CurrentAppointmentPage()
        {
            InitializeComponent();

            current = new AppointmentModel();

            loadingLabel.IsVisible = true;
            detailsFrame.IsVisible = false;

            OnAppearing();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var appointmentLogic = new AppointmentLogic();
            await appointmentLogic.Init();

            var patient = await PatientLogic.GetPatientAsync(General.UserId);

            if (appointmentLogic.AllAppointments.Any(a => a.PatientName == PatientLogic.GetFullName(patient.FirstName, patient.Surname) &&
                 !a.Complete && a.PatientID == patient.UserID))
            {
                current = appointmentLogic.GetCurrentAppointment(patient, appointmentLogic.AllAppointments);

                titleLabel.IsVisible = false;
                loadingLabel.IsVisible = false;
                detailsFrame.IsVisible = true;
                dateSpan.Text = current.AppointmentTime.ToLongDateString() + ", ";
                timeSpan.Text = current.AppointmentTime.ToShortTimeString();
                symptomsSpan.Text = current.symtoms;

                if (current.Confirmed)
                {
                    statusSpan.TextColor = Color.Green;
                    statusSpan.Text = "confirmed";
                }
                else
                {
                    statusSpan.TextColor = Color.Red;
                    statusSpan.Text = "Pending";
                }
            }
            else
            {
                titleLabel.IsVisible = true;
                titleLabel.Text = "You don't have an appointment booked.";
                titleLabel.HorizontalTextAlignment = TextAlignment.Center;
                loadingLabel.IsVisible = false;
                detailsFrame.IsVisible = false;
            }
        }

        private async void cancelBtn_Clicked(object sender, EventArgs e)
        {
            var date = current.AppointmentTime;

            await AppointmentLogic.CancelAppointmentAsync(current);

            await DisplayAlert("Appointment", string.Format("You cancelled your appointment on {0} at {1}", date.ToLongDateString(), date.ToShortTimeString()), "Okay");

            OnAppearing();
        }
    }
}