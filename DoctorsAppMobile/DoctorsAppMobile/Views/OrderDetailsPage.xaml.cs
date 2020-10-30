﻿using DoctorsAppMobile.Logic;
using DoctorsAppMobile.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace DoctorsAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailsPage : ContentPage
    {
        OrderDetailPageModel details;
        ProductLogic productLogic;

        public OrderDetailsPage(OrderDetailPageModel model)
        {
            InitializeComponent();

            productLogic = new ProductLogic();

            details = model;
            details.CartItemsModel = new System.Collections.Generic.List<CustomerCartViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await productLogic.Init();
            var cartItems = details.CartItems;

            details.CartItemsModel = (from items in cartItems
                                      join products in productLogic.Products on items.ProductId equals products.Id
                                      select new CustomerCartViewModel
                                      {
                                          Name = string.Format("{0} ({1})", products.Name, items.Quantity),
                                          Size = products.PackSize,
                                          Total = items.Price
                                      }).ToList();

            BindData();

            cartListView.ItemsSource = details.CartItemsModel;
        }

        private void BindData()
        {
            orderIdLabel.Text = details.Id.ToString();
            paymentMethodLabel.Text = details.PaymentMethod.ToString();
            orderDateLabel.Text = details.OrderDate.ToString("dd/MM/yyyy");
            totalLabel.Text = "R" + details.Total.ToString("n2");
            nameLabel.Text = details.Name.ToString();
            addressLabel.Text = details.Address;
            cityLabel.Text = details.City;
            emailLabel.Text = details.Email;
            phoneNumberLabel.Text = details.PhoneNumber;
            provinceSpan.Text = details.Province + ", ";
            zipSpan.Text = details.ZipCode;
            countryLabel.Text = details.Country;
            statusLabel.Text = details.StatusName;
        }

        private void confirmButton_Clicked(object sender, System.EventArgs e)
        {
            Scanner();
        }

        public async void Scanner()
        {

            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) =>
            {
                ScannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    // Logic to confirm order delivery in here

                    Navigation.PopAsync();
                    DisplayAlert("Hello", result.Text, "OK");
                });
            };

            await Navigation.PushAsync(ScannerPage);

        }
    }
}