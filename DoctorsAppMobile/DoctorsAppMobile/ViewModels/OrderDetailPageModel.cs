using DoctorsAppMobile.Models;
using System;
using System.Collections.Generic;

namespace DoctorsAppMobile.ViewModels
{
    public class OrderDetailPageModel
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public float Total { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string StatusName { get; set; }

        public List<CustomerCartModel> CartItems { get; set; }
        public List<CustomerCartViewModel> CartItemsModel { get; set; }
    }
    public class CustomerCartViewModel
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public float Total { get; set; }
    }
}
