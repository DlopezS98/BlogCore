using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Ingresar un nombre a la categoría")]
        [Display(Name = "Nombre Categoría")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Orden de Visualización")]
        public string Order { get; set; }
    }
}