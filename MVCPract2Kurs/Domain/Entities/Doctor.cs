using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public ICollection<DoctorClinic> DoctorClinics { get; set; }
        public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; }

    }
}
