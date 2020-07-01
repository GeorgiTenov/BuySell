using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Prodai.Data;
using ProductLibrary;

namespace Prodai.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProdaiContext _db;

        private readonly IWebHostEnvironment _host;
        public ProductController(ProdaiContext db,IWebHostEnvironment host)
        {
            this._db = db;
            this._host = host;
        }
        public IActionResult Index()
        {
            return View();
        }
        [RequestSizeLimit(2000000)]
        public async Task<IActionResult> Article(Product product,IFormFile picture1,
                                                 IFormFile picture2,IFormFile picture3)
        {
            SaveFileToProduct(product);
            product.Date = DateTime.Now;
            product.ArticleName = ArticleName.Продавам;
            SavepictureToProductIfNotNull(product,picture1,picture2,picture3);
            SaveProductToDb(product);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ArticleBuy()
        {
           
            return View();
        }

        [RequestSizeLimit(2000000)]
        public IActionResult BuyFinished(Product product)
        {
                SaveFileToProduct(product);
                product.ArticleName = ArticleName.Купувам;
                product.Date = DateTime.Now;
                SaveProductToDb(product);
                return RedirectToAction("Index", "Home");
                
        }
        public IActionResult ShowCategories(ProductsEnum category,Cities city,string keySearch)
        {
            //if (keySearch == null)
            //{
            //    return Content("Не е намерен артикул с тази ключава дума");
            //}
          
            if (keySearch != null)
            {

                var ordered = ShowAllProductsWithCategory(category, city, keySearch)
                .OrderByDescending(c => c.Date);

                if (ordered.Count() <= 0)
                {
                    return View("Views/Home/NotFoundSearch.cshtml");
                }
                 return View(ordered);
               
            }
            return View(ShowAllProductsWithCategory(category, city, keySearch));
            
        }

        public IActionResult Details(int? id)
        {
            Product product = null;
            if(id != null)
            {
                 product = GetAllProductsFromDb().FirstOrDefault(p => p.Id == id);
            }
            return View(product);
        }

       

        private void SaveProductToDb(Product product)
        {
            this._db.Products.Add(product);
            this._db.SaveChanges();
        }

        private List<Product> GetAllProductsFromDb()
        {
            return this._db.Products.ToList();
        }

        private void SaveFileToProduct(Product product)
        {
            if(product.ProductIcon != null)
            {
                string root = this._host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product.ProductIcon.FileName);
                string extension = Path.GetExtension(product.ProductIcon.FileName);
                string fullPath = Path.Combine(root + "/Pictures/", fileName + extension);
                product.ProductIconName = fileName + extension;
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    product.ProductIcon.CopyTo(fileStream);
                }
            }
          
        }
        private List<Product> ShowAllProductsWithCategory(ProductsEnum category,Cities city,string keySearch)
        {
            List<Product> products = new List<Product>();
            List<Product> keyLess = new List<Product>();
            foreach(var item in this._db.Products)
            {
                if(item.Category == category && item.City == city)
                {
                    keyLess.Add(item);
                }
            }

            foreach (var product in this._db.Products)
            {
                if(product != null)
                {
                    if(keySearch != null)
                    {
                        if (product.Category == category
                       && product.City == city
                       && product.Name.ToLower().Contains(keySearch.ToLower()))
                        {
                            products.Add(product);
                        }
                    }
                   
                    if(keySearch == null)
                    {
                        return keyLess;
                    }
                }
            }
            return products;
        }

        private void RemoveProductsFromDb()
        {
            this._db.Products.RemoveRange(this._db.Products);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Add more images and save them to Pictures folder
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="image"></param>
        private void AddMoreImages(string imageName,IFormFile image)
        {
            if(image != null)
            {
                string root = this._host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                string extension = Path.GetExtension(image.FileName);
                string fullPath = Path.Combine(root + "/Pictures/", fileName + extension);
                imageName = fileName + extension;
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    image.CopyToAsync(fileStream);
                }
            }
          
           
        }
        private void SavepictureToProductIfNotNull(Product product,IFormFile picture1,IFormFile picture2,IFormFile picture3)
        {
            if (picture1 != null)
            {
                product.Pictures.Add(picture1);
                AddMoreImages(product.PictureName1, picture1);
                product.PictureName1 = Path.GetFileName(picture1.FileName);
            }

            if (picture2 != null)
            {
                product.Pictures.Add(picture2);
                AddMoreImages(product.PictureName2, picture2);
                product.PictureName2 = Path.GetFileName(picture2.FileName);
            }

            if (picture3 != null)
            {
                product.Pictures.Add(picture3);
                AddMoreImages(product.PictureName3, picture3);
                product.PictureName3 = Path.GetFileName(picture3.FileName);
            }

        }
       
    }
}