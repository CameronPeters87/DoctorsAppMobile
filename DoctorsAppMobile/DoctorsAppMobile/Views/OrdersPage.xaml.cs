using DoctorsAppMobile.Constants;
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
        OrderLogic orderLogic = new OrderLogic();
        OrderStatusLogic statusLogic = new OrderStatusLogic();
        CartLogic cartLogic = new CartLogic();

        public OrdersPage()
        {
            InitializeComponent();

            orderLogic = new OrderLogic();
            statusLogic = new OrderStatusLogic();
            cartLogic = new CartLogic();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await orderLogic.Init();
            await statusLogic.Init();
            await cartLogic.Init();

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

            General.AllOrders = orderLogic.AllOrders;
            General.AllCart = cartLogic.AllCart;

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
            var orderPage = (OrdersPageModel)e.SelectedItem;

            var orderDetail = (from order in orderLogic.MyOrders
                               join status in statusLogic.Statuses on order.OrderStatusId equals status.Id
                               select new OrderDetailPageModel
                               {
                                   Id = order.Id,
                                   Address = order.Address,
                                   City = order.City,
                                   Country = order.Country,
                                   Email = order.Email,
                                   Name = order.FirstName + " " + order.Surname,
                                   OrderDate = order.OrderDate,
                                   PaymentMethod = order.PaymentMethod,
                                   PhoneNumber = order.PhoneNumber,
                                   ZipCode = order.ZipCode,
                                   StatusName = status.Name,
                                   Total = order.TotalCost
                               }).FirstOrDefault();

            orderDetail.CartItems = cartLogic.AllCart.Where(c => c.CustomerOrderId == orderDetail.Id &&
                c.CustomerId == General.UserId).ToList();

            General.OrderSpecificCartItems = orderDetail.CartItems;

            Navigation.PushAsync(new OrderDetailsPage(orderDetail));
        }
    }
}