﻿using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Logic;
using DoctorsAppMobile.ViewModels;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            await details.Init(productLogic.Products);

            General.Streams = details.ImageStreams;

            details.CartItemsModel = (from items in cartItems
                                      join products in productLogic.Products on items.ProductId equals products.Id
                                      select new CustomerCartViewModel
                                      {
                                          ImgUrl = new System.Uri(General.URL + products.ImageUrl),
                                          ImgStream = new MemoryStream(Encoding.UTF8.GetBytes(General.URL + products.ImageUrl)),
                                          Streams = General.Streams,
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
        }

        private void confirmButton_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}