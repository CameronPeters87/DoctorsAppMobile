using DoctorsAppMobile.Logic;
using DoctorsAppMobile.ViewModels;
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
            var statusLogic = new OrderStatusLogic();

            await orderLogic.Init();
            await statusLogic.Init();

            var orderList = (from orders in orderLogic.MyOrders
                             join os in statusLogic.Statuses on orders.OrderStatusId equals os.Id
                             orderby orders.Id descending
                             select new OrdersPageModel
                             {
                                 Id = orders.Id,
                                 OrderDate = orders.OrderDate,
                                 Status = os.Name,
                                 TotalCost = orders.TotalCost,
                                 StatusId = os.Id
                             }).ToList();

            ordersListView.ItemsSource = orderList;
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
            Navigation.PushAsync(new OrderDetailsPage());
        }
    }
}