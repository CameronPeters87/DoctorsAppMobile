using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

    }
}
