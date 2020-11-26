using DoctorsAppMobile.Logic;
using DoctorsAppMobile.Models;
using DoctorsAppMobile.ViewModels;
using System;
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
        DeliveryLogic deliveryLogic;
        DriverLogic driverLogic;
        OrderLogic orderLogic;
        OrderStatusLogic statusLogic;

        public OrderDetailsPage(OrderDetailPageModel model)
        {
            InitializeComponent();

            productLogic = new ProductLogic();
            deliveryLogic = new DeliveryLogic();
            driverLogic = new DriverLogic();
            orderLogic = new OrderLogic();
            statusLogic = new OrderStatusLogic();

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
            if (details.StatusName != "On it's way!")
            {
                await DisplayAlert("Order #" + details.Id, "You cannot confirm order delivery for this order.", "OK");
                await Navigation.PopAsync();
            }

            await deliveryLogic.Init();
            await driverLogic.Init();
            await orderLogic.Init();
            await statusLogic.Init();

            var delivery = deliveryLogic.GetDelivery(details, deliveryLogic.AllDeliveries);
            var driver = driverLogic.GetDriver(delivery, driverLogic.AllDrivers);
            var order = orderLogic.AllOrders.Where(o => o.Id == details.Id).FirstOrDefault();
            var completedStatus = statusLogic.Statuses.Where(s => s.Name == "Completed").FirstOrDefault();

            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) =>
            {
                ScannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Logic to confirm order delivery in here
                    if (result.Text == delivery.QRCodeTextConfirmation)
                    {
                        await Navigation.PopAsync();
                        await ConfirmAsync(delivery, driver, order, completedStatus);
                        await DisplayAlert("Order #" + details.Id, "You successfully confirmed delivery!", "OK");

                    }
                    else
                    {
                        await Navigation.PopAsync();
                        await DisplayAlert("Order #" + details.Id, "Not the right QR to confirm delivery.", "OK");
                    }
                });
            };

            await Navigation.PushAsync(ScannerPage);

        }

        private async System.Threading.Tasks.Task ConfirmAsync(DeliveryApiModel delivery, DriverApiModel driver, CustomerOrderModel order, OrderStatusModel completedStatus)
        {
            // Delivery
            delivery.Status = "Completed";
            delivery.ConfirmationType = "Confirmed by QR Scan";
            delivery.TimeDelivered = DateTime.Now;

            // Driver
            driver.Status = "Active";

            // Order
            order.OrderStatus = completedStatus;
            order.OrderStatusId = completedStatus.Id;

            await deliveryLogic.UpdateDelivery(delivery);
            await driverLogic.UpdateDriver(driver);
            await orderLogic.UpdateOrder(order);

        }
    }
}