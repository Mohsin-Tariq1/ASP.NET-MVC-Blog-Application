using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EAD_Assignment_3.Models;

namespace EAD_Assignment_3.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View("../User/SignIn");
        }
    }
}
