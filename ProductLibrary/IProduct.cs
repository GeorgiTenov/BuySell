using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary
{
    public interface IProduct
    {  
        int Id { get; set; }
        ArticleName ArticleName { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        string Description { get; set; }
        string ProductIconName { get; set; }
        IFormFile ProductIcon { get; set; }
        ProductsEnum Category { get; set; }
        DateTime Date { get; set; }
        Cities City { get; set; }
        
    }
}
