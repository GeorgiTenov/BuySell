using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Text;

namespace ProductLibrary
{
    public class Product : IProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Име на продукта")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1200,ErrorMessage ="Описанието трябва да съдържа до 1200 символа")]
        [DisplayName("Описание на продукта")]
        public string Description { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [Column]
        [MaxLength(1300)]
        [Required]
        [DisplayName("Адрес,телефон за връзка и/или електронна поща")]
        public string Contact { get; set; }

        [Required]
        [DisplayName("Снимка на продукта")]
        [NotMapped]
        public IFormFile ProductIcon { get; set; }

        public string ProductIconName { get; set; }

        [Column]
        [Required]
        [DisplayName("Категория")]
        public ProductsEnum Category { get; set; }
       
        [Column]
        [NotMapped]
        public List<IFormFile> Pictures = new List<IFormFile>();

        [Column]
        public string PictureName1 { get; set; }

        [Column]
        public string PictureName2 { get; set; }

        [Column]
        public string PictureName3 { get; set; }
        
        [Column]
        [DisplayName("Тип на обявата")]
        public ArticleName ArticleName { get;set; }
        [Column]
        [DisplayName("Дата на обявата")]
        public DateTime Date { get; set; } = DateTime.Now;

        
        [Column]
        [DisplayName("Град")]
        public Cities City { get; set; }
    }
}
