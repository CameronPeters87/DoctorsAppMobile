using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Views;
using Xamarin.Forms;

namespace DoctorsAppMobile
{
    public partial class MainPage : TabbedPage
    {
        public MainPage(int userId)
        {
            InitializeComponent();

            General.UserId = userId;
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new WebPage());
        }
    }
}
