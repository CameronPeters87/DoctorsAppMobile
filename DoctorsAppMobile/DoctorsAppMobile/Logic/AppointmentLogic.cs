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
    public class AppointmentLogic
    {
        List<AppointmentModel> model;
        List<AppointmentModel> sorted;

        public List<AppointmentModel> AllAppointments
        {
            get
            {
                return model;
            }
        }

        public List<AppointmentModel> AvailableAppointments
        {
            get
            {
                return sorted;
            }
        }


        public async Task Init()
        {
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
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            sorted = model.Where(m => string.IsNullOrEmpty(m.PatientName)).ToList();
        }

        public static async Task UpdateCustomerAsync(AppointmentModel model)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient(httpClientHandler))
            {
                Uri uri = new Uri(string.Format("{0}/Appointments", General.BASE_URL));
                try
                {
                    HttpResponseMessage responseMessage = await client.PutAsync(uri, content);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public AppointmentModel GetCurrentAppointment(PatientModel patient, List<AppointmentModel> allAppointments)
        {
            return allAppointments.Where(a => a.PatientName == PatientLogic.GetFullName(patient.FirstName, patient.Surname)
                && !a.Complete)
                .FirstOrDefault();
        }
    }
}
