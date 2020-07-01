using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Prodai.Data;
using Prodai.Models;
using ProductLibrary;

namespace Prodai.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ProdaiContext _db;

        private IMemoryCache _cache;

        public HomeController(ILogger<HomeController> logger,ProdaiContext db,IMemoryCache cache)
        {
            _logger = logger;
            _db = db;
            _cache = cache;
        }

        public IActionResult Index()
        {
            //DeleteAllFromDB();
            if (GetAllProducts().Count() > 0)
            {
                var productsImages = this._db.Products.OrderByDescending(p => p.Date).ToList();
                ViewBag.ProductsImages = productsImages;
                if (!this._cache.TryGetValue("products", out productsImages))
                {
                    _cache.Set("products", _db.Products.ToList());
                    ViewBag.ProductsImages = _cache.Get("products") as List<Product>;
                }
               
                DeleteExpiredArticles();
               //DeleteAllFromDB();
                return View();
            }
            return View();
        }

       public IActionResult ShowSellProducts()
        {
            return View(AllSellProducts());
        }

        public IActionResult ShowBuyProducts()
        {
            return View(AllBuyProducts());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult About()
        {
            return View();
        }
        //Get all products from database
        private List<Product> GetAllProducts()
        {
            var products = this._db.Products.ToList();
            return products;
        }

        //Checking if all products from db are empty or not
        private bool IsEmpty()
        {
            if(GetAllProducts().Count() > 0)
            {
                return false;
            }
            return true;
        }

        //List for all sell products
        private List<Product> AllSellProducts()
        {
            List<Product> productSells = new List<Product>();
            foreach(var product in this._db.Products)
            {
                if(product.ArticleName == ArticleName.Продавам)
                {
                    productSells.Add(product);
                }
            }
            var orderedSells = productSells.OrderByDescending(p => p.Date);
            return orderedSells.ToList();
        }

        //List for all buy products
        private List<Product> AllBuyProducts()
        {
            List<Product> productBuys = new List<Product>();
            foreach (var product in this._db.Products)
            {
                if (product.ArticleName == ArticleName.Купувам)
                {
                    productBuys.Add(product);
                }
            }
            var orderedBuys = productBuys.OrderByDescending(p => p.Date);
            return orderedBuys.ToList();
        }

        private Product ImageHomePage()
        {
            Product product = null;
           List<Product>products = this._db.Products.ToList();
            if(products.Count() > 0)
            {
                 product = products.OrderByDescending(p => p.Date).FirstOrDefault();
               
            }
            return product;
        }
        private void DeleteExpiredArticles()
        {
            var now = DateTime.Now;
            var products = this._db.Products.ToList();
            foreach(var p in products)
            {
                if(p.Date > now.AddMonths(1))
                {
                    DeleteProduct(p);
                }
            }
        }
        private void DeleteProduct(Product product)
        {
            this._db.Products.Remove(product);
            this._db.SaveChanges();
        }
        private void DeleteAllFromDB()
        {
            this._db.Products.RemoveRange(this._db.Products);
            this._db.SaveChanges();
        }
    }
}
