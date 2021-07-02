using electronics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace electronics.Infrastructure
{
    public interface IProduct
    {
        IEnumerable<Computer> GetAll();
        Computer GetById(int computerId);
        void Insert(Computer computer);

        void Update(Computer computer);
        void Delete(Computer computer);
      

    }
}
