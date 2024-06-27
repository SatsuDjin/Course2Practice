using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }

        [Required]
        [Display(Name = "ФИО доктора")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Фото(название с расширением, например:pgg.jpg)")]
        public string Photo { get; set; }

        // Свойства для выбора клиник и специализаций
        public List<int> SelectedClinicIds { get; set; }
        public List<int> SelectedSpecializationIds { get; set; }

        // Инициализация списков
        public List<Clinic> AvailableClinics { get; set; } = new List<Clinic>();
        public List<Specialization> AvailableSpecializations { get; set; } = new List<Specialization>();
        
    }




}

