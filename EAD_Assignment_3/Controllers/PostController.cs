using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAD_Assignment_3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAD_Assignment_3.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        public ViewResult PostForm(int id)
        {
            ViewBag.id = id;
            return View();
        }
        public ViewResult PostDetails(int postUId)
        {
            int? userId = HttpContext.Session.GetInt32("signIn");
            PostRepository pR = new PostRepository();
            Post post = pR.getPost(userId);
            return View("PostDetails",post);
        }
        [HttpPost]
        public ViewResult PostForm(Post p,int id)
        {
            ViewBag.id = id;
            PostRepository pR = new PostRepository();

            if (ModelState.IsValid)
            {
                string check = pR.AddPost(p,id);
                if (check.Equals("post added"))
                {
                    List<Post> posts = pR.getAllPosts();
                    return View("../User/HomeScreen",posts);
                }
                else if (check.Equals("post not added"))
                {
                    ModelState.AddModelError(String.Empty, "Your post could not be added.Try again!");
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
        public ViewResult Remove(int id)
        {
            PostRepository pR = new PostRepository();
            int? userId = HttpContext.Session.GetInt32("signIn");
            bool result = pR.deletePost(userId);
            if(result)
            {
                List<Post> posts = pR.getAllPosts();
                return View("../User/HomeScreen", posts);
            }
            List<Post> post = pR.getAllPosts();
            return View("../User/HomeScreen", post);
        }

        public ViewResult Edit()
        {
            PostRepository pr = new PostRepository();
            int? userId = HttpContext.Session.GetInt32("signIn");
            Post post = pr.getPost(userId);
            return View("Edit", post);

        }

        [HttpPost]
        public ViewResult Edit(Post p)
        {
            if (ModelState.IsValid)
            {
                UserRepository uR = new UserRepository();
                PostRepository pR = new PostRepository();
                bool result =pR.updatePost(p);
                if (result)
                {
                    List<Post> posts = pR.getAllPosts();
                    return View("../User/HomeScreen", posts);
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
