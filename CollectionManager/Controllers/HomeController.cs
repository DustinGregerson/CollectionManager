using CollectionManager.Models;
using CollectionManager.tools;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

//Author: Dustin Gregerson
//Date: 11/29/2023
//Description: handels the routing for the main page

namespace CollectionManager.Controllers
{
    public class HomeController : Controller
    {

        private CollectersContext context;
        public HomeController(CollectersContext ctx)
        {
            context = ctx;
        }
        public ViewResult Index()
        {
            //Grabs the last item added to the items data set and displays it on the main page
            var result= from items in context.items
                        orderby items.itemID
                        select new {Itempic=items.image,ItemDescription=items.Description,ItemName=items.Name };
            var last=result.Last();
            string base64 = imageConverter.byteArrayTo64BaseEncode(last.Itempic);
            ViewBag.frontPageName = last.ItemName;
            ViewBag.frontPageImage = base64;
            ViewBag.frontPageDescription = last.ItemDescription;
            return View();
        }

    }
}