using FribergCarRentals.Models;
using FribergCarRentals.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace FribergCarRentals.Controllers
{
    

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //GET: Home/Index
        public IActionResult Index()
        {
            
            return View();
        }

        //GET: Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        //GET: Home/HomePage
        public IActionResult HomePage()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
