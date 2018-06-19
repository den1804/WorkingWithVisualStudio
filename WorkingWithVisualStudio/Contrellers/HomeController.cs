using Microsoft.AspNetCore.Mvc;
using WorkingWithVisualStudio.Models;
using System.Linq;

namespace WorkingWithVisualStudio.Contrellers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
            => View(SimpleRepository.SharedRepository.Products
                .Where(p => p?.Price < 50));
    }
}
