using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class SpecializationViewModel
    {
        public int SpecializationId { get; set; }
        [Required(ErrorMessage = "Имя специальности обязательно.")]
        [Display(Name = "Название специальности")]
        public string Name { get; set; }
        public int DoctorCount { get; set; }

    }
}
