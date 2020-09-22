using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCore.Models
{
    public class ArticleModel
    {
        [Key]
        public int ArticleID {get; set;}

        [Required(ErrorMessage = "El nombre del articulo es obligatorio")]
        [Display(Name = "Nombre del Articulo")]
        public string ArticleName {get; set;}

        [Display(Name = "Fecha de creación")]
        public string ArticleCreationDate {get; set;}

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string ArticleImageUrl {get; set;}

        [Required(ErrorMessage ="Descripción obligatoria")]
        [Display(Name = "Descripción")]
        public string ArticleDescription {get; set;}

        [Required]
        public int Fk_CategoryID {get; set;}

        [ForeignKey("Fk_CategoryID")]
        public CategoryModel Category {get; set;}
    }
}