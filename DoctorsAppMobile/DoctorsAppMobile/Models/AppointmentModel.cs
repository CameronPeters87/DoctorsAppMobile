using DoctorsAppMobile.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoctorsAppMobile.Models
{
    public class AppointmentModel
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentTime { get; set; }
        public bool Confirmed { get; set; }
        public bool CheckedIn { get; set; }
        public string symtoms { get; set; }
        public string diagnosis { get; set; }
        public DateTime Expire { get; set; }
        public bool isPaid { get; set; }
        public bool Complete { get; set; }
        public PatientModel PatientModel { get; set; }

        List<AppointmentModel> model;

        public List<AppointmentModel> AppointmentModels
        {
            get
            {
                return model;
            }
        }

        public async Task Initialise()
        {
            model = new List<AppointmentModel>();

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
        }

    }
}
