using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Models;
using DoctorsAppMobile.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsAppMobile.Logic
{
    public class DeliveryLogic
    {
        List<DeliveryApiModel> model;

        public List<DeliveryApiModel> AllDeliveries
        {
            get
            {
                return model;
            }
        }

        public async Task Init()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            using (var client = new HttpClient(httpClientHandler))
            {
                Uri uri = new Uri(string.Format("{0}/Deliveries", General.BASE_URL));
                try
                {
                    var responseMessage = await client.GetAsync(uri);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var content = responseMessage.Content
                            .ReadAsStringAsync().Result;

                        model = JsonConvert.DeserializeObject<List<DeliveryApiModel>>(content);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public DeliveryApiModel GetDelivery(OrderDetailPageModel order, List<DeliveryApiModel> deliveries)
        {
            return deliveries.Where(d => d.OrderId == order.Id).FirstOrDefault();
        }

        public async Task UpdateDelivery(DeliveryApiModel delivery)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var json = JsonConvert.SerializeObject(delivery);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = new Uri(General.BASE_URL + "/");
                try
                {
                    HttpResponseMessage responseMessage = await client.PutAsync("Deliveries", content);
                    string test = responseMessage.ToString();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
