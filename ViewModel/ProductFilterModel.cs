using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace electronics.ViewModel
{
    public class ComputerFilterModel
    {
       
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public IEnumerable<int?> Discount { get; set; }
        public IEnumerable<int?> RamSize { get; set; }
        public IEnumerable<string> ComputerBrand { get; set; }
        public IEnumerable<string> MaximumResolution { get; set; }
        public IEnumerable<string> SreenSize { get; set; }
        public IEnumerable<string> StorageType { get; set; }
        public IEnumerable<string> GraphicCard { get; set; }
        public IEnumerable<string> Processor { get; set; }
        public IEnumerable<string> OperatingSystem { get; set; }
      
    }
}
