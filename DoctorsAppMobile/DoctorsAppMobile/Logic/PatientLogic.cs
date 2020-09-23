using DoctorsAppMobile.Constants;
using DoctorsAppMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoctorsAppMobile.Logic
{
    public class PatientLogic
    {

        public static async Task<List<PatientModel>> GetPatients()
        {
            var patients = new List<PatientModel>();

            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            using (var client = new HttpClient(httpClientHandler))
            {
                Uri uri = new Uri(string.Format("{0}/Patients", General.BASE_URL));
                try
                {
                    var responseMessage = await client.GetAsync(uri);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var content = responseMessage.Content
                            .ReadAsStringAsync().Result;

                        patients = JsonConvert.DeserializeObject<List<PatientModel>>(content);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return patients;
        }

        public static async Task<PatientModel> GetPatientAsync(int id)
        {
            var patients = new List<PatientModel>();

            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            using (var client = new HttpClient(httpClientHandler))
            {
                Uri uri = new Uri(string.Format("{0}/Patients", General.BASE_URL));
                try
                {
                    var responseMessage = await client.GetAsync(uri);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var content = responseMessage.Content
                            .ReadAsStringAsync().Result;

                        patients = JsonConvert.DeserializeObject<List<PatientModel>>(content);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            var patient = patients.Where(p => p.UserID == id).FirstOrDefault();

            return patient;
        }

        public static string GetFullName(string first_name, string surname)
        {
            return string.Format("{0} {1}", first_name, surname);
        }
    }
}
