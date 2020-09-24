using DoctorsAppMobile.Logic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorsAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        public OrdersPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var orderLogic = new OrderLogic();

            await orderLogic.Init();

            ordersListView.ItemsSource = orderLogic.MyOrders.OrderByDescending(o => o.OrderDate).ToList(); ;
            ordersListView.ItemSelected += OrdersListView_ItemSelected;
            ordersListView.Refreshing += OrdersListView_Refreshing;
        }

        private void OrdersListView_Refreshing(object sender, System.EventArgs e)
        {
            OnAppearing();
            ordersListView.IsRefreshing = false;
        }

        private void OrdersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}