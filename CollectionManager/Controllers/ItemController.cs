﻿using CollectionManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CollectionManager.Controllers
{
    public class ItemController : Controller
    {
        private UsersContext context;
        public ItemController(UsersContext ctx)
        {
            context = ctx;
        }
        public IActionResult List()
        {
            return View();
        }
        
        public IActionResult Detailed(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add() {
            Item item = new Item();
            
                String name = HttpContext.Request.Form["name"];
                String description = HttpContext.Request.Form["description"];
                IFormFile image = HttpContext.Request.Form.Files["imageFile"];
                String tag = HttpContext.Request.Form["tag"];
                String id = HttpContext.Request.Form["id"];
                int error = 0;
                if (name.Equals(""))
                {
                    error += 0x1;
                }
                if (description.Equals(""))
                {
                    error += 0x2;
                }
                if (image==null || image.Length==0)
                {
                    error += 0x4;
                }
                if(tag.Equals(""))
                {
                    error += 0x8;
                }
                if(id.Equals(""))
                {
                    error += 0x10;
                }
            if (error == 0)
            {
                User user = context.users.Find(id);
                if (user != null)
                {
                    item.Description = description;
                    byte[] imageData;
                    using(var stream = new MemoryStream())
                    {
                        image.CopyTo(stream);
                        imageData = stream.ToArray();
                    }
                    item.image= imageData;
                    item.tag = tag;
                    item.Name= name;
                    item.user= user;


                    context.items.Add(item);
                    return View("Detailed",item);
                }
                else
                {
                    return RedirectToAction("login","User");
                }
            }
            else
            {
                return View("Add",error);
            }
        }
    }
}
