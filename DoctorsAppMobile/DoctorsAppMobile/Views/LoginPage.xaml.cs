using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Logic;
using DoctorsAppMobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var client = new System.Net.Http.HttpClient(httpClientHandler);
            System.IO.Stream imagestream = await client.GetStreamAsync(General.URL + "/Content/title.jpg");
            imageLogin.Source = ImageSource.FromStream(() => imagestream);
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