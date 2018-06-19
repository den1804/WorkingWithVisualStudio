using Microsoft.AspNetCore.Mvc;
using WorkingWithVisualStudio.Models;

namespace WorkingWithVisualStudio.Contrellers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
            => View(SimpleRepository.SharedRepository.Products);
    }
}
