using electronics.Data;
using electronics.Infrastructure;
using electronics.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using X.PagedList;

namespace electronics.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationContext _context;
        public ProductController(IProduct product, IWebHostEnvironment hostEnvironment, ApplicationContext context)
        {
            _product = product;
            _context = context;
            webHostEnvironment = hostEnvironment;

        }
        [HttpGet]
        public IActionResult Index(int? page)
        {

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login","Admin");
            }
            var products = _product.GetAll();
            var pageNumber = page ?? 1;
            var pageSize = 15;
            var onePageOfProducts = products.OrderByDescending(i => i.Id).ToPagedList(pageNumber, pageSize);
            return View(onePageOfProducts);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(_product.GetById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Computer computer)
        {
            string uniqImageName = null;
            var productGaleries = new List<ProductGalery>();
            if (computer.Discount != null)
            {
                computer.DiscountedPrice = computer.Price.Discount(computer.Discount);
            }

            List<string> uniqueFileName = UploadImage(computer);

            foreach (var image in uniqueFileName)
            {
                uniqImageName = image;

                ProductGalery productGalery = new ProductGalery
                {
                    ImageUrl = uniqImageName,
                    Computer = computer,
                };
                productGaleries.Add(productGalery);
            }
            _context.Galeries.AddRange(productGaleries);
            if (computer != null)
                _product.Insert(computer);

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(_product.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Computer computer)
        {
            string uniqImageName;
            var productGaleries = new List<ProductGalery>();
            if (computer.Discount != null)
            {
                computer.DiscountedPrice = computer.Price.Discount(computer.Discount);
            }
            List<string> uniqueFileName = UploadImage(computer);

            foreach (var image in uniqueFileName)
            {
                uniqImageName = image;

                ProductGalery productGalery = new ProductGalery
                {
                    ImageUrl = uniqImageName,
                    Computer = computer,
                };
                productGaleries.Add(productGalery);
            }
            _context.Galeries.AddRange(productGaleries);
            _product.Update(computer);
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(_product.GetById(id));
        }

        [HttpPost]
        public IActionResult Delete(Computer computer)
        {
            _product.Delete(computer);
            return RedirectToAction("index");
        }

        private List<string> UploadImage(Computer product)
        {
            List<string> uniqueFileName = new List<string>();
            string filePath = "images";

            if (product.ProductImages != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, filePath);
                foreach (IFormFile item in product.ProductImages)
                {

                    var FileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    uniqueFileName.Add(FileName);
                    filePath = Path.Combine(uploadsFolder, FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }
    }
}
