//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CollectionJoy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Stocks = new HashSet<Stock>();
        }
    
        public int ID { get; set; }
        [Required(ErrorMessage ="Please, introduce a name")]
        [DisplayName("Product")]
        public string Name { get; set; }
        [Range(0,30000, ErrorMessage =" Price can not be negative. Please, insert a number above 0")]
        public Nullable<int> Price { get; set; }
        [Required(ErrorMessage = "Please, select a category")]
        public int Category { get; set; }
        public byte[] Image { get; set; }
        [HiddenInput(DisplayValue=false)]
        public string ImageMimeType { get; set; }
    
        public virtual Category Category1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}