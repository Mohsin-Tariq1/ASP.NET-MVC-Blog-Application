using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAD_Assignment_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace EAD_Assignment_3.Controllers
{
    public class AdminController : Controller
    {
        public ViewResult Remove(int id)
        {
            UserRepository uR = new UserRepository();
            bool result = uR.deleteUser(id);
            if (result)
            {
                List<User> users = uR.getAllUsers();
                return View("AdminForm", users);
            }
            List<User> user = uR.getAllUsers();
            return View("AdminForm",user);
        }

        public ViewResult Edit(int id)
        {
            UserRepository uR = new UserRepository();
            User u = uR.getUser(id);
            return View("Edit", u);

        }
        [HttpPost]
        public ViewResult Edit(User u)
        {
            if (ModelState.IsValid)
            {
                UserRepository uR = new UserRepository();
                bool result = uR.updateUser(u);
                if(result)
                {
                    List<User> users = uR.getAllUsers();
                    return View("AdminForm", users);
                }
                return View("AdminForm");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Please enter correct data");
                return View();
            }
        }
        public ViewResult Add()
        {
            return View("../User/SignUp");
        }

        [HttpPost]
        public ViewResult SignUp(User u)
        {
            UserController uC = new UserController();
            uC.SignUp(u);
            UserRepository uR = new UserRepository();
            List<User> users = uR.getAllUsers();
            return View("AdminForm", users);
        }

        public ViewResult Logout()
        {
            return View("../User/SignIn");
        }
    }
}
