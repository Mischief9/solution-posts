using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Posts.Controllers
{
    public class HomeController : Controller
    {
        static PostController obj = new PostController();
        
        public IActionResult Index()
        {
            ViewBag.List = obj.ReturnList();    //ლოკალური პოსტების ლისტი. PostController-ში.
            return View();
        }

    }
}