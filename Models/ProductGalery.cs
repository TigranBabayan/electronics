using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace electronics.Models
{
    public class ProductGalery
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
       
        public Computer Computer { get; set; }

        public string ImageUrl { get; set; }
    }
}
