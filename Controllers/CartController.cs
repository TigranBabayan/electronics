using electronics.Infrastructure;
using electronics.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace electronics.Controllers
{
    public class CartController : Controller
    {
        private readonly IProduct _product;

        public CartController(IProduct product)
        {
            _product = product;
        }


        public IActionResult Index()
        {
            var cart = Extention.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            var carts = cart;
            if(cart!=null)
            ViewBag.total = cart.Sum(item => item.Computer.Price * item.Quantity);
            return View(carts);
        }

        [Route("add/{id}")]
        public IActionResult Add(int id)
        {
             Computer comp = new Computer();
            if (Extention.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { Computer =_product.GetById(id) , Quantity = 1 });
                Extention.SetObjectAsJson<List<Cart>>(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Cart> cart = Extention.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Cart { Computer = _product.GetById(id), Quantity = 1 });
                }
                Extention.SetObjectAsJson<List<Cart>>(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index","Home");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Cart> cart = Extention.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            Extention.SetObjectAsJson<List<Cart>>(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Cart> cart = Extention.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Computer.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
