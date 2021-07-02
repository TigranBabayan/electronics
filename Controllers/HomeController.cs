using electronics.Data;
using electronics.Infrastructure;
using electronics.Models;
using electronics.ViewModel;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace electronics.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _product;
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeController(IProduct product, IWebHostEnvironment hostEnvironment, ApplicationContext context)
        {
            _product = product;
            _context = context;
            webHostEnvironment = hostEnvironment;

        }
        public IActionResult Index(int? page)
        {
            var products = _product.GetAll();
            var pageNumber = page ?? 1;
            var pageSize = 9;
            var onePageOfProducts = products.OrderByDescending(i => i.Id).ToPagedList(pageNumber, pageSize);
            ViewBag.Discounts = products.Where(d => d.Discount > 0).Select(d => d.Discount).Distinct().OrderBy(i => i).ToList();
            ViewBag.ComputerBrand = products.Where(d => d.ComputerBrand != null).Select(d => d.ComputerBrand).Distinct().OrderBy(i => i).ToList();
            ViewBag.Processor = products.Where(d => d.Processor != null).Select(d => d.Processor).Distinct().OrderBy(i => i).ToList();
            ViewBag.OperatingSystem = products.Where(d => d.OperatingSystem != null).Select(d => d.OperatingSystem).Distinct().OrderBy(i => i).ToList();
            ViewBag.RamSize = products.Where(d => d.RamSize != 0).Select(d => d.RamSize).Distinct().OrderBy(i => i).ToList();
            ViewBag.StorageType = products.Where(d => d.StorageType != null).Select(d => d.StorageType).Distinct().OrderBy(i => i).ToList();
            ViewBag.GraphicCard = products.Where(d => d.GraphicCard != null).Select(d => d.GraphicCard).Distinct().OrderBy(i => i).ToList();
            ViewBag.MaximumResolution = products.Where(d => d.MaximumResolution != null).Select(d => d.MaximumResolution).Distinct().OrderBy(i => i).ToList();
            ViewBag.SreenSize = products.Where(d => d.SreenSize != null).Select(d => d.SreenSize).Distinct().OrderBy(i => i).ToList();
            return View(onePageOfProducts);
        }
        public IActionResult Details(int id)
        {
            var product = _product.GetById(id);
            ////ViewBag.SimilarComputers = _product.GetAll().Where(i => i.Processor==thisLaptop.Processor && i.RamSize==thisLaptop.RamSize);//նմանատիպ համակարգիչներ 
            return View(product);
        }

        [HttpPost]
        public IActionResult Filter(ComputerFilterModel model, int? page)
        {
            var computers = new List<Computer>();
            var ResultPredicate = PredicateBuilder.New<Computer>();

            if (model.MinPrice != null && model.MaxPrice == null)
            {
                var MinMaxPricePredicate = PredicateBuilder.New<Computer>();
                MinMaxPricePredicate = MinMaxPricePredicate.And(p => p.Price >= model.MinPrice);
                ResultPredicate.And(MinMaxPricePredicate);
            }
            if (model.MinPrice == null && model.MaxPrice != null)
            {
                var MinMaxPricePredicate = PredicateBuilder.New<Computer>();
                MinMaxPricePredicate = MinMaxPricePredicate.And(p => p.Price <= model.MaxPrice);
                ResultPredicate.And(MinMaxPricePredicate);
            }
            if (model.MinPrice != null && model.MaxPrice != null)
            {
                var MinMaxPricePredicate = PredicateBuilder.New<Computer>();
                MinMaxPricePredicate = MinMaxPricePredicate.And(p => p.Price >= model.MinPrice && p.Price <= model.MaxPrice);
                ResultPredicate.And(MinMaxPricePredicate);
            }

            if (model.Discount != null)
            {
                var DiscountPredicate = PredicateBuilder.New<Computer>();
                foreach (var discount in model.Discount)
                {
                    DiscountPredicate = DiscountPredicate.Or(p => p.Discount == discount);
                }
                ResultPredicate.And(DiscountPredicate);
            }

            if (model.ComputerBrand != null)
            {
                var ComputerBrandPredicat = PredicateBuilder.New<Computer>();
                foreach (var brand in model.ComputerBrand)
                {
                    ComputerBrandPredicat = ComputerBrandPredicat.Or(p => p.ComputerBrand == brand);
                }
                ResultPredicate.And(ComputerBrandPredicat);
            }

            if (model.RamSize != null)
            {
                var RamSizePredicate = PredicateBuilder.New<Computer>();
                foreach (var ram in model.RamSize)
                {
                    RamSizePredicate = RamSizePredicate.Or(p => p.RamSize == ram);
                }
                ResultPredicate.And(RamSizePredicate);
            }
            if (model.MaximumResolution != null)
            {
                var MaximumResolutionPredicate = PredicateBuilder.New<Computer>();
                foreach (var resolution in model.MaximumResolution)
                {
                    MaximumResolutionPredicate = MaximumResolutionPredicate.Or(p => p.MaximumResolution == resolution);
                }
                ResultPredicate.And(MaximumResolutionPredicate);
            }
            if (model.OperatingSystem != null)
            {
                var OperatingSystemPredicate = PredicateBuilder.New<Computer>();
                foreach (var os in model.OperatingSystem)
                {
                    OperatingSystemPredicate = OperatingSystemPredicate.Or(p => p.OperatingSystem == os);
                }
                ResultPredicate.And(OperatingSystemPredicate);
            }
            if (model.SreenSize != null)
            {
                var SreenSizePredicate = PredicateBuilder.New<Computer>();
                foreach (var size in model.SreenSize)
                {
                    SreenSizePredicate = SreenSizePredicate.Or(p => p.SreenSize == size);
                }
                ResultPredicate.And(SreenSizePredicate);
            }
            if (model.StorageType != null)
            {
                var StorageTypePredicate = PredicateBuilder.New<Computer>();
                foreach (var storegType in model.StorageType)
                {
                    StorageTypePredicate = StorageTypePredicate.Or(p => p.StorageType == storegType);
                }
                ResultPredicate.And(StorageTypePredicate);
            }
            if (model.GraphicCard != null)
            {
                var GraphicCardPredicate = PredicateBuilder.New<Computer>();
                foreach (var graphiccard in model.GraphicCard)
                {
                    GraphicCardPredicate = GraphicCardPredicate.Or(p => p.GraphicCard == graphiccard);
                }
                ResultPredicate.And(GraphicCardPredicate);
            }
            if (model.Processor != null)
            {
                var ProcessorPredicate = PredicateBuilder.New<Computer>();
                foreach (var processor in model.Processor)
                {
                    ProcessorPredicate = ProcessorPredicate.Or(p => p.Processor == processor);
                }
                ResultPredicate.And(ProcessorPredicate);
            }


            computers = _context.Computers.Where(ResultPredicate).Include(img => img.ProductGaleries).ToList();
            var unchekedProducts = _context.Computers.Include(img => img.ProductGaleries).ToList();

            var pageNumber = page ?? 1;
            var pageSize = 9;
            var onePageOfProducts = computers.ToPagedList(pageNumber, pageSize);
            var unchekedOnePageOfProducts = unchekedProducts.ToPagedList(pageNumber, pageSize);
            if (model.Discount == null && model.ComputerBrand == null && model.MaxPrice == null && model.MinPrice == null && model.Processor == null
                && model.MaximumResolution == null && model.RamSize == null && model.SreenSize == null && model.StorageType == null && model.GraphicCard == null && model.OperatingSystem==null)
            {
                return RedirectToAction("Index", unchekedOnePageOfProducts);
            }
            var products = _product.GetAll();
            ViewBag.Discounts = products.Where(d => d.Discount > 0).Select(d => d.Discount).Distinct().OrderBy(i => i).ToList();
            ViewBag.ComputerBrand = products.Where(d => d.ComputerBrand != null).Select(d => d.ComputerBrand).Distinct().OrderBy(i => i).ToList();
            ViewBag.Processor = products.Where(d => d.Processor != null).Select(d => d.Processor).Distinct().OrderBy(i => i).ToList();
            ViewBag.OperatingSystem = products.Where(d => d.OperatingSystem != null).Select(d => d.OperatingSystem).Distinct().OrderBy(i => i).ToList();
            ViewBag.RamSize = products.Where(d => d.RamSize != 0).Select(d => d.RamSize).Distinct().OrderBy(i => i).ToList();
            ViewBag.StorageType = products.Where(d => d.StorageType != null).Select(d => d.StorageType).Distinct().OrderBy(i => i).ToList();
            ViewBag.GraphicCard = products.Where(d => d.GraphicCard != null).Select(d => d.GraphicCard).Distinct().OrderBy(i => i).ToList();
            ViewBag.MaximumResolution = products.Where(d => d.MaximumResolution != null).Select(d => d.MaximumResolution).Distinct().OrderBy(i => i).ToList();
            ViewBag.SreenSize = products.Where(d => d.SreenSize != null).Select(d => d.SreenSize).Distinct().OrderBy(i => i).ToList();

            return View("FilteredView", onePageOfProducts);
        }

    }

}
