using Microsoft.AspNetCore.Mvc;
using WorkingWithVisualStudio.Models;
using System.Linq;

namespace WorkingWithVisualStudio.Contrellers
{
    public class HomeController : Controller
    {
        SimpleRepository repository = SimpleRepository.SharedRepository;
        public IActionResult Index()
            => View(repository.Products.Where(p => p?.Price < 50));

        [HttpGet]
        public IActionResult AddProduct() => View(new Product());

        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            repository.AddProduct(p);
            return RedirectToAction("Index");
        }

    }
   
}
