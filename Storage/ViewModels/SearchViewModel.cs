using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.ViewModels
{
    public class SearchViewModel
    {
        public List<Product> Products { get; set; }
        public SelectList Category { get; set; }
        public string CategoryId { get; set; }
        public string SearchString { get; set; }
        public Product Product { get; set; }
    }
}
