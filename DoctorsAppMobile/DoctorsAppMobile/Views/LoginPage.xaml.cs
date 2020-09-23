using DoctorsAppMobile.Logic;
using DoctorsAppMobile.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorsAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        List<PatientModel> patients;

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            patients = await PatientLogic.GetPatients();
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            bool loggedIn = false;
            foreach (var patient in patients)
            {
                if (emailEntry.Text == patient.Email && passwordEntry.Text == patient.Password)
                {
                    loggedIn = true;
                    await Navigation.PushAsync(new MainPage(patient.UserID));
                }
            }

            if (!loggedIn)
            {
                await DisplayAlert("Failed", "Invalid login attempt", "Try again");
            }
        }
    }
}