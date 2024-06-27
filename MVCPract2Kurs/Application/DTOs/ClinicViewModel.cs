using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ClinicViewModel
    {
        public int ClinicId { get; set; }

        [Required]
        [Display(Name = "Название клиники")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Города")]
        public int CityId { get; set; }
        [Display(Name = "Адресс")]
        public string Address { get; set; }
        [Display(Name = "Фото(название с расширением, например:pgg.jpg)")]
        public string Photo { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }

}
