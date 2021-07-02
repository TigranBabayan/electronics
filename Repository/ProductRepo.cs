using electronics.Data;
using electronics.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using electronics.Models;
using Microsoft.EntityFrameworkCore;

namespace electronics.Repository
{
    public class ProductRepo : IProduct 
    {
        private readonly ApplicationContext _context;
      

        public ProductRepo(ApplicationContext context)
        {
            _context = context;

         
        }

        public void Insert(Computer computer)
        {
            
            _context.Computers.Add(computer);
            _context.SaveChanges();
           
        }



        public void Delete(Computer computer)
        {
            _context.Computers.Remove(computer);
            _context.SaveChanges();
        }

        public IEnumerable<Computer> GetAll()
        {
            return _context.Computers.Include(img => img.ProductGaleries).ToList();
        }

        public Computer GetById(int productId)
        {
          
            return _context.Computers.Include(img => img.ProductGaleries).Where(i => i.Id == productId).FirstOrDefault();
        }


        public void Update(Computer product)
        {
            _context.Computers.Update(product);
            _context.SaveChanges();
        }
    }
}
