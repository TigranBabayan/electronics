using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Transactions;

namespace electronics.Models
{
    [DataContract]
    public class Computer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Display(Name = "Ապրանքանիշ")]
        public string ComputerBrand { get; set; }
        [DataMember]
        [Display(Name = "Մոդել")]
        public string ComputerModel { get; set; }
        [DataMember]
        [Display(Name = "Օպերացիոն համակարգ")]
        public string OperatingSystem { get; set; }
        [DataMember]
        [Display(Name = "Օպերատիվ հիշասարք")]
        public int RamSize { get; set; }
        [DataMember]
        [Display(Name = "HDD")]
        public int? HddCapacity { get; set; }
        [DataMember]
        [Display(Name = "SSD")]
        public int? SsdCapacity { get; set; }
        [DataMember]
        [Display(Name = "Պրոցեսոր")]
        public string Processor { get; set; }
        [DataMember]
        [Display(Name = "Գրաֆիկական քարտ")]
        public string GraphicCard { get; set; }
        [DataMember]
        [Display(Name = "Էկրանի կետայնություն")]
        public string MaximumResolution { get; set; }
        [DataMember]
        [Display(Name = "Պրոցեսորի արագություն")]
        public string ProcessorSpeed { get; set; }
        [DataMember]
        [Display(Name = "Հիշողության տեսակ")]
        public string StorageType { get; set; }
        [DataMember]
        [Display(Name = "Գույն")]
        public string Color { get; set; }
        [DataMember]
        [Display(Name = "Էկրանի չափս")]
        public string SreenSize { get; set; }
        [DataMember]
        [Display(Name = "Գին")]
        public int Price { get; set; }
        [DataMember]
        [Display(Name = "Զեղչված գին")]
        public int? DiscountedPrice { get; set; }
        [DataMember]
        [Display(Name = "Նոր")]
        public bool IsNew { get; set; }
        [DataMember]
        [Display(Name = "Զեղչ")]
        public int? Discount { get; set; }

        [NotMapped]
        [Display(Name = "Նկար")]
        public IEnumerable<IFormFile> ProductImages { get; set; }
     
        public IEnumerable<ProductGalery> ProductGaleries { get; set; }


    }
}
