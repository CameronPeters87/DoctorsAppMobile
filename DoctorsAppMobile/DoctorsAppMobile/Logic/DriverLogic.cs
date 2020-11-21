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
    public class DriverLogic
    {
        List<DriverApiModel> model;

        public List<DriverApiModel> AllDrivers
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
                Uri uri = new Uri(string.Format("{0}/Drivers", General.BASE_URL));
                try
                {
                    var responseMessage = await client.GetAsync(uri);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var content = responseMessage.Content
                            .ReadAsStringAsync().Result;

                        model = JsonConvert.DeserializeObject<List<DriverApiModel>>(content);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public async Task UpdateDriver(DriverApiModel driver)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var json = JsonConvert.SerializeObject(driver);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = new Uri(General.BASE_URL + "/");
                try
                {
                    HttpResponseMessage responseMessage = await client.PutAsync("Drivers", content);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public DriverApiModel GetDriver(DeliveryApiModel delivery, List<DriverApiModel> drivers)
        {
            return drivers.Where(d => d.Id == delivery.DriverId).FirstOrDefault();
        }
    }
}
