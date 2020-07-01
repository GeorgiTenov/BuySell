using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductLibrary
{
    public enum ProductsEnum
    {

        [Display(Name = "Стоки за бита")]
        HouseholdProducts,

        [Display(Name ="Обувки")]
        Shoes,

        [Display(Name = "Дрехи")]
        Clothes,

        [Display(Name ="Техника")]
        Technology,
        
        [Display(Name ="Домашни любимци")]
        Pets,

        [Display(Name = "Играчки")]
        Toys,

        [Display(Name ="Автомобилни аксесоари")]
        AutomobileParts,

        [Display(Name ="Други")]
        Others

       


    }
}
