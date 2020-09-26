using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoctorsAppMobile.Logic
{
    public class ProductLogic
    {
        List<ProductModel> products;

        public List<ProductModel> Products
        {
            get
            {
                return products;
            }
        }

        public async Task Init()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            using (var client = new HttpClient(httpClientHandler))
            {
                Uri uri = new Uri(string.Format("{0}/Products", General.BASE_URL));
                try
                {
                    var responseMessage = await client.GetAsync(uri);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var content = responseMessage.Content
                            .ReadAsStringAsync().Result;

                        products = JsonConvert.DeserializeObject<List<ProductModel>>(content);
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
