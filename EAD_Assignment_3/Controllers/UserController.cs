using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EAD_Assignment_3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace EAD_Assignment_3.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ViewResult SignUp()
        {

            return View();
        }
        [HttpGet]
        public ViewResult SignIn()
        {

            return View();
        }
        [HttpPost]
        public ViewResult SignUp(User u)
        {
            UserRepository uR=new UserRepository();
            
            if (ModelState.IsValid)
            {
                string check = uR.AddUser(u);
                if(check.Equals("user added"))
                {
                    return View("SignIn");
                }
                else if (check.Equals("user exist"))
                {
                    ModelState.AddModelError(String.Empty, "This username already exist.");
                    return View();
                }
                else if (check.Equals("user not added"))
                {
                    ModelState.AddModelError(String.Empty, "Please try again!");
                    return View();
                }
                else if (check.Equals("password not matched"))
                {
                    ModelState.AddModelError(String.Empty, "Password not matched");
                    return View();
                }
                return View();
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Please enter correct data");
                return View();
            }

        }
        [HttpPost]
        public ViewResult SignIn(User1 u)
        {
            UserRepository uR = new UserRepository();
            PostRepository pR = new PostRepository();
            if (ModelState.IsValid)
            {
                var value = uR.SignInUser(u);
                if (value.check.Equals("user exist"))
                {
                    int userId = uR.getId(u.Name);
                    HttpContext.Session.SetInt32("signIn", userId);
                    ViewBag.id = value.id;
                    List<Post> posts = pR.getAllPosts();
                    return View("HomeScreen",posts);
                }
                else if (value.check.Equals("admin"))
                {
                    List<User> users = uR.getAllUsers();
                    return View("../Admin/AdminForm", users);
                }
                else if (value.check.Equals("user not exist"))
                {
                    ModelState.AddModelError(String.Empty, "Your password or username is wrong.");
                    return View();
                }
                return View();
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Please enter correct data");
                return View();
            }

        }
        public ViewResult HomeScreen()
        {
            PostRepository pR = new PostRepository();
            List<Post> posts = pR.getAllPosts();
            return View("HomeScreen", posts);
        }

        public ViewResult About()
        {
            return View();
        }
        public ViewResult Edit()
        {
            User u = new User();
            if (HttpContext.Session.Keys.Contains("signIn"))
            {
                UserRepository user = new UserRepository();
                int? userId = HttpContext.Session.GetInt32("signIn");
                u= user.getUser(userId);
            }
            return View("Edit", u);
        }

        [HttpPost]
        public ViewResult Edit(User u)
        {
            if (ModelState.IsValid)
            {
                UserRepository uR = new UserRepository();
                PostRepository pR = new PostRepository();
                bool result = uR.updateUser(u);
                if (result)
                {
                    List<Post> posts = pR.getAllPosts();
                    return View("HomeScreen", posts);
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Try Again.");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Please enter correct data");
                return View();
            }
        }
    }
}
