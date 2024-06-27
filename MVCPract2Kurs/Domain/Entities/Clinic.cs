using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Clinic
    {
        public int ClinicId { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public ICollection<DoctorClinic> DoctorClinics { get; set; }
    }
}
