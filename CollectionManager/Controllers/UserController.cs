using CollectionManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web;
using CollectionManager.Models;
using CollectionManager.tools;
using System.Collections.Generic;

namespace CollectionManager.Controllers
{
    public class UserController : Controller
    {
       private CollectersContext context;
        public UserController(CollectersContext ctx)
        {
            context = ctx;
        }

        public IActionResult Login()
        {
            Login login=new Login();
            login.loginError = 0;
            login.createAccountError = 0;
            return View(login);
        }
        [HttpPost]
        public IActionResult attemptLogin(Login model) {
            model.loginError = 0;
            if(model.userName==null)
            {
                model.loginError += 0x1;
            }
            if(model.passWord==null)
            {
                model.loginError += 0x2;
            }
            if (model.loginError==0)
            {
                User user = context.users.SingleOrDefault(m => m.userName==model.userName);
                if (user != null&&user.password.Equals(model.passWord))
                {
                    HttpContext.Session.SetString("id", ""+user.userID);
                    HttpContext.Session.SetString("userName", user.userName);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    model.loginError += 0x4;
                    return View("Login",model);
                }
            }
            else
            {
                return View("Login",model);
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("userName", "null");
            return RedirectToAction("index", "Home");
        }
        public IActionResult Details()
        {
            List<ItemsToUsersList> list = new List<ItemsToUsersList>();
            var result = from item in context.items
                          join user in context.users on item.userID equals user.userID
                          where user.userName == HttpContext.Session.GetString("userName")
                          select new { ItemName = item.Name, ItemDescription = item.Description, ItemTag = item.tag, UserName = user.userName, ItemPic = item.image, ItemId = item.itemID };
            foreach (var row in result)
            {
                ItemsToUsersList item = new ItemsToUsersList();
                item.name = row.ItemName;
                item.description = row.ItemDescription;
                item.tag = row.ItemTag;
                item.userName = row.UserName;
                item.itemId = "" + row.ItemId;
                item.pic = imageConverter.byteArrayTo64BaseEncode(row.ItemPic);
                list.Add(item);
            }
            return View(list);
        }
        [HttpPost]
        public IActionResult CreateAccount(String userName,String passWord)
        {
            var currentUser= context.users.Find(userName);
            if (currentUser == null)
            {

            }
            else
            {
                HttpContext.Session.SetString("userName", userName);
                currentUser.userName = userName;
                currentUser.password = passWord;
                context.users.Add(currentUser);
            }

            return View();
        }
    }
}
