using DoctorsAppMobile.Models;
using System.Collections.Generic;

namespace DoctorsAppMobile.Constants
{
    public class General
    {
        public const string BASE_URL = "https://192.168.8.47:45456/api";
        public const string URL = "https://192.168.8.47:45456";

        public static int UserId { get; set; }

        public static List<OrderStatusModel> Statuses { get; set; }
        public static List<CustomerOrderModel> AllOrders { get; set; }
        public static List<CustomerOrderModel> MyOrders { get; set; }
        public static List<CustomerCartModel> AllCart { get; set; }
        public static List<CustomerCartModel> OrderSpecificCartItems { get; set; }

        public static List<AppointmentModel> AllAppointments { get; set; }
        public static List<AppointmentModel> AvailableAppointments { get; set; }


    }
}
