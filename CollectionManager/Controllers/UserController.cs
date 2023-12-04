using CollectionManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web;
using CollectionManager.Models;
using CollectionManager.tools;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

//Author: Dustin Gregerson
//Date: 11/29/2023
//Description: handels the routing for the users
namespace CollectionManager.Controllers
{
    public class UserController : Controller
    {
       private CollectersContext context;
        public UserController(CollectersContext ctx)
        {
            context = ctx;
        }
        
        public ViewResult Login()
        {
            //creates a login model and assigns the errors to 0
            Login login=new Login();
            login.loginError = 0;
            login.createAccountError = 0;
            return View(login);
        }
        [HttpPost]
        public IActionResult attemptLogin(Login model) {
            //validates that the username and password fields were filled in
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
                //checks to see if the user name and password match a user in the user data set
                if (user != null&&user.password.Equals(model.passWord))
                {
                    //sets session variables that are used later
                    HttpContext.Session.SetString("id", ""+user.userID);
                    HttpContext.Session.SetString("userName", user.userName);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    //incorrect username or password return with the login model
                    model.loginError += 0x4;
                    return View("Login",model);
                }
            }
            else
            {
                //if the fields were blank return back to login with login model
                return View("Login",model);
            }
        }
        public IActionResult LogOut()
        {
            //clear the session data for the user
            HttpContext.Session.SetString("userName", "null");
            HttpContext.Session.SetString("id", "");
            return RedirectToAction("index", "Home");
        }
        public ViewResult Details()
        {
            //creats a lists of ItemsToUsersList models and returns them to the view. it will contain
            //all of the items that the loggin user has added to there collection on the site.
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
        public ViewResult Create(Error error)
        {
            //check if the error model has beem created
            //if not add it view
            if (error == null)
            {
                Error errorBuild = new Error();
                error.errorNumber = 0;
                return View(errorBuild);
            }
            else
            {
                return View(error);
            }
        }
        [HttpPost]
        public IActionResult CreateAccount()
        {
            //get the username and password strings from the form
            string userName = HttpContext.Request.Form["userName"];
            string passWord = HttpContext.Request.Form["passWord"];

            //check if the user with the username provided already exsists
            User currentUser = context.users.SingleOrDefault(u => u.userName == userName);

            //creates a new error model
            Error error =new Error();
            error.errorNumber=0;

            if (currentUser == null)
            {
                //is user name blank
                if (userName.IsNullOrEmpty())
                {
                    error.errorNumber += 0x2;
                }
                //is password blank
                if(passWord.IsNullOrEmpty())
                {
                    error.errorNumber += 0x4;
                }
                //if not create the new user.
                if (error.errorNumber == 0)
                {
                    User user = new User();
                    user.userName = userName;
                    user.password = passWord;
                    HttpContext.Session.SetString("userName", userName);
                    context.users.Add(user);
                    context.SaveChanges();
                    currentUser = context.users.SingleOrDefault(u => u.userName == userName);
                    HttpContext.Session.SetString("id",""+currentUser.userID);
                    return RedirectToAction("index", "Home");
                }
                //if blank field display the error messages
                else
                {
                    return View("Create",error);
                }
            }
            //if the user exists display the error message
            else
            {
                error.errorNumber += 0x1;
                return View("Create",error);
            }
        }
    }
}
