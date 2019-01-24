using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Models;
using Storage.ViewModels;

namespace Storage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StorageContext _context;

        public ProductsController(StorageContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SummaIndex()
        {
            
            var Productssum = new List<ProductViewModel>();
            var products = await _context.Product.ToListAsync();
            
            foreach (var item in products)
            {
                var ps = new ProductViewModel();
                ps.Name = item.Name;
                ps.Price = item.Price;
                ps.Count = item.Count;
                ps.Sum = item.Count * item.Price;
                Productssum.Add(ps);

            }
      

            return View(Productssum);
        }
        public async Task<IActionResult> NothingInStorage()
        {

            var model = await _context.Product.Where(i => i.Count == 0).ToListAsync();


            return View(model);


        
        }

        public async Task<IActionResult> Electronics()
        {
            var model = await _context.Product.Where(i => i.Category == "Electronics").ToListAsync();
          

            return View(model);
        }
        public async Task<IActionResult> FilterCategory()
        {
            var vm = new SearchViewModel();

            var categories = new List<string>();

            var products = await _context.Product.ToListAsync();
            foreach (var item in products)
            {
                if (!categories.Exists(x => (x == item.Category))) categories.Add(item.Category);

            }
            var sellist = new List<SelectListItem>();
            int i = 1;
            foreach (var item in categories)
            {
                var selitem = new SelectListItem();
                selitem.Text = item;
                selitem.Selected = false;
                selitem.Value = item;
                sellist.Add(selitem);
                i++;
            }


            vm.Category = new SelectList(categories);


            vm.Products = products;

            vm.SearchString = vm.Category.DataValueField;

            //  return View(await _context.Product.ToListAsync());
            return View(vm);
        }







              [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterCategory(string CategoryId)
            {
                var vm = new SearchViewModel();

                var categories = new List<string>();

                var products = await _context.Product.ToListAsync();
                foreach (var item in products)
                {
                    if (!categories.Exists(x => (x == item.Category))) categories.Add(item.Category);

                }
                var sellist = new List<SelectListItem>();
                int j = 1;
                foreach (var item in categories)
                {
                    var selitem = new SelectListItem();
                    selitem.Text = item;
                
                    selitem.Selected = false;
                    selitem.Value = item;
                    sellist.Add(selitem);
                    j++;
                }


                vm.Category = new SelectList(categories);


                vm.Products = products;

                vm.SearchString = CategoryId;

           vm.Products = await _context.Product.Where(i => i.Category == CategoryId).ToListAsync();

            //  return View(await _context.Product.ToListAsync());
            return View(vm);


            }

        








        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ListCategories(string category )
        {
            var model = await _context.Product.Where(i => i.Category == category).ToListAsync();


            return View(model);
        }
    }
}
