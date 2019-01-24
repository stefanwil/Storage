using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
       [Range(1, 200,
            ErrorMessage = "Price must be between 1 and 100")]

        public int Price { get; set; }
       [DataType(DataType.Date)]//tillhäör raden efter
        [Display(Name= "Order Date")]  //gör om presentation av namn
        public DateTime Orderdate { get; set; }
        public string Category { get; set; }
        public string Shelf { get; set; }
      [Range(0, 100,
           ErrorMessage = "Count must be between 0 and 200")]
        public int Count { get; set; }
        public string Description { get; set; }
    }
}
