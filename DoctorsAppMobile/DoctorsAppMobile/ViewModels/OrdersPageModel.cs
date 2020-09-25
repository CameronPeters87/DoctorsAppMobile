using System;

namespace DoctorsAppMobile.ViewModels
{
    public class OrdersPageModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalCost { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }

        public string StatusColor
        {
            get
            {
                if (Status == "Completed")
                {
                    return "Green";
                }
                else
                    return "Blue";
            }
        }
    }
}
