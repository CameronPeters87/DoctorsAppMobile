using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoctorsAppMobile.ViewModels
{
    public class AppointmentViewModel
    {
        List<AppointmentModel> availableAppointments;

        public IEnumerable<AppointmentModel> AppointmentModels
        {
            get
            {
                return availableAppointments;
            }
        }

        public async Task Initialise()
        {
            List<AppointmentModel> model = new List<AppointmentModel>();

            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            using (var client = new HttpClient(httpClientHandler))
            {
                Uri uri = new Uri(string.Format("{0}/Appointments", General.BASE_URL));
                try
                {
                    var responseMessage = await client.GetAsync(uri);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var content = responseMessage.Content
                            .ReadAsStringAsync().Result;

                        model = JsonConvert.DeserializeObject<List<AppointmentModel>>(content);

                        availableAppointments = GetAvailableAppointments(model);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private List<AppointmentModel> GetAvailableAppointments(List<AppointmentModel> model)
        {
            return model.Where(m => !m.Complete).ToList();
        }
    }
}
