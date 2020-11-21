using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsAppMobile.Logic
{
    public class OrderLogic
    {
        List<CustomerOrderModel> allOrders;
        List<CustomerOrderModel> myOrders;

        public List<CustomerOrderModel> MyOrders
        {
            get
            {
                return myOrders;
            }
        }

        public List<CustomerOrderModel> AllOrders
        {
            get
            {
                return allOrders;
            }
        }

        public async Task Init()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            using (var client = new HttpClient(httpClientHandler))
            {
                Uri uri = new Uri(string.Format("{0}/CustomerOrders", General.BASE_URL));
                try
                {
                    var responseMessage = await client.GetAsync(uri);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var content = responseMessage.Content
                            .ReadAsStringAsync().Result;

                        allOrders = JsonConvert.DeserializeObject<List<CustomerOrderModel>>(content);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            myOrders = allOrders.Where(o => o.CustomerId == General.UserId).ToList();
        }

        public async Task UpdateOrder(CustomerOrderModel order)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var json = JsonConvert.SerializeObject(order);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = new Uri(General.BASE_URL + "/");
                try
                {
                    HttpResponseMessage responseMessage = await client.PutAsync("CustomerOrders", content);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

    }
}
