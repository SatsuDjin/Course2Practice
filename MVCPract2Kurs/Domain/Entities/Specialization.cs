using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Specialization
    {
        public int SpecializationId { get; set; }
        public string Name { get; set; }
        public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; }
    }
}
