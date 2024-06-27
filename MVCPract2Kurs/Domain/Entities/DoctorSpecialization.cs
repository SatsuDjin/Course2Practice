using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DoctorSpecialization
    {
        [Key]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
