using DoctorsAppMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorsAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailsPage : ContentPage
    {
        OrderDetailPageModel details;
        public OrderDetailsPage(OrderDetailPageModel model)
        {
            InitializeComponent();
            details = model;
            details.CartItemsModel = new System.Collections.Generic.List<CustomerCartViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //details.CartItemsModel = (from cart in details.CartItems
            //                          join product in )

            //cartListView.ItemsSource = model.CartItems;
        }
    }
}