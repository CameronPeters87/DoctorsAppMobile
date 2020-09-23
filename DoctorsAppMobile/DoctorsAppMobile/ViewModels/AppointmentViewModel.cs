using DoctorsAppMobile.Models;
using System.Collections.Generic;
using System.Linq;

namespace DoctorsAppMobile.ViewModels
{
    public class AppointmentViewModel
    {
        List<AppointmentModel> availableAppointments;

        public IEnumerable<AppointmentModel> Appointments
        {
            get
            {
                return availableAppointments;
            }
        }

        public AppointmentViewModel()
        {
        }

        public AppointmentViewModel(AppointmentModel model)
        {
            availableAppointments = GetAvailableAppointments(model.AppointmentModels);
        }

        private List<AppointmentModel> GetAvailableAppointments(List<AppointmentModel> model)
        {
            return model.Where(m => string.IsNullOrEmpty(m.PatientName)).ToList();
        }
    }
}
