using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

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


        List<System.IO.Stream> imageStreams;
        public List<Stream> ImageStreams { get; set; }
        public async Task Init(List<ProductModel> products)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var client = new System.Net.Http.HttpClient(httpClientHandler);

            foreach (var item in products)
            {
                foreach (var cartItem in CartItems)
                {
                    if (item.Id == cartItem.ProductId)
                    {
                        var stream = await client.GetStreamAsync(General.URL + item.ImageUrl);
                        imageStreams.Add(stream);
                    }
                }
            }
        }
    }
    public class CustomerCartViewModel
    {
        public Uri ImgUrl { get; set; }
        public Stream ImgStream { get; set; }
        public List<Stream> Streams { get; set; }
        public ImageSource ImageSource
        {
            get
            {

                return ImageSource.FromStream(() => ImgStream);
            }
        }

        public string Name { get; set; }
        public string Size { get; set; }
        public float Total { get; set; }


    }
}
