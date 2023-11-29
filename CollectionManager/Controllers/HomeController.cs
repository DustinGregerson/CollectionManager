using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

//Author: Dustin Gregerson
//Date: 11/29/2023
//Description: main routing controller

namespace CollectionManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}