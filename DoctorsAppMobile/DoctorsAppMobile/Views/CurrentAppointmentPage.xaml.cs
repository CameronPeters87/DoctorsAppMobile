﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorsAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentAppointmentPage : ContentPage
    {
        public CurrentAppointmentPage()
        {
            InitializeComponent();
        }

        private void cancelBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}