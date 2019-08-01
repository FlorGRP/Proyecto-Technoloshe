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
    using System.ComponentModel.DataAnnotations;

    public partial class Stock
    {
        public int ID { get; set; }
        public int Product { get; set; }
        public int Size { get; set; }
        [Range(0, 30000, ErrorMessage = " Quantity can not be negative. Please, insert a number above 0")]
        public Nullable<int> Quantity { get; set; }
    
        public virtual Product Product1 { get; set; }
        public virtual Size Size1 { get; set; }
    }
}