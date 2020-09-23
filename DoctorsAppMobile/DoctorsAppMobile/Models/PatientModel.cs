using DoctorsAppMobile.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoctorsAppMobile.Models
{
    public class PatientModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public bool isLoyal { get; set; }
        public int AddressId { get; set; }
        public AddressModel Address { get; set; }

        
    }
}
