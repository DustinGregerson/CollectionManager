using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

//Author: Dustin Gregerson
//Date: 11/29/2023
//Description: main routing controller

namespace CollectionManager.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}