using CollectionManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web;
using CollectionManager.Models;
namespace CollectionManager.Controllers
{
    public class UserController : Controller
    {
       private UsersContext context;
        public UserController(UsersContext ctx)
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
                var user = context.users.SingleOrDefault(m => m.userName==model.userName);
                if (user != null&&user.password.Equals(model.passWord))
                {
                    HttpContext.Session.SetString("userName", model.userName);
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
            return View();
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
