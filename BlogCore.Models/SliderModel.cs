using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class SliderModel
    {
        [Key]
        public int Pk_SliderID { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre")]
        public string SliderName { get; set; }

        [Required(ErrorMessage = "Estado requerido")]
        [Display(Name = "Estado")]
        public bool SliderStatus { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen del Slider")]
        public string SliderImageUrl { get; set; }

    }
}