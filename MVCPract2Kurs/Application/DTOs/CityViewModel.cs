using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CityViewModel
    {
        public int CityId { get; set; }

        [Required(ErrorMessage = "Имя города обязательно.")]
        [Display(Name = "Название города")]
        public string Name { get; set; }
    }
}
