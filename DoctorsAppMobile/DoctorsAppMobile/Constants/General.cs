using DoctorsAppMobile.Models;
using System.Collections.Generic;
using System.IO;

namespace DoctorsAppMobile.Constants
{
    public class General
    {
        public const string BASE_URL = "http://192.168.8.47:45455/api";
        public const string URL = "http://192.168.8.47:45455";

        //public const string BASE_URL = "http://2020grp29drjames.azurewebsites.net/api";
        //public const string URL = "http://2020grp29drjames.azurewebsites.net";

        public static int UserId { get; set; }

        public static List<OrderStatusModel> Statuses { get; set; }
        public static List<CustomerOrderModel> AllOrders { get; set; }
        public static List<CustomerOrderModel> MyOrders { get; set; }
        public static List<CustomerCartModel> AllCart { get; set; }
        public static List<CustomerCartModel> OrderSpecificCartItems { get; set; }

        public static List<AppointmentModel> AllAppointments { get; set; }
        public static List<AppointmentModel> AvailableAppointments { get; set; }
        public static List<Stream> Streams { get; set; }

    }
}
