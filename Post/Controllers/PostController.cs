using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Posts.Models;

namespace Posts.Controllers
{
    public class PostController : Controller
    {
        static List<PostModel> posts=new List<PostModel>();

        [HttpGet]
        [Route("/Post/Posts")]
        public IActionResult Posts()            //პოსტები
        {
            ViewBag.List = posts;               //View-ს აძლევს პოსტების Lists
            return View("Posts");               
        }


        public IActionResult AddPostForm()
        {
            ViewBag.Button = "დამატება";    //html ფორმა დამატების Button-ით.
            return View("AddPostForm");
        }

        [HttpPost]
        [Route("Post/AddPost")]
        public IActionResult AddPost(PostModel post)
        {
            ViewBag.InvalidURL = "";
            if (post.ID > 0) ViewBag.Button = "რედაქტირება";            // განსაზღვრა რედაქტირებაა თუ დამატება პოსტის.
            else ViewBag.Button = "დამატება";
            if(ModelState.IsValid && !post.ValidImageURL())         // Image ლინკი არის თუარა სწორი. ModelState.IsValid არის იმისთვის რომ ლინკ არიკოს ცარიელი.
            {
                ViewBag.InvalidURL = "სურათის მისამართი არასწორია";        
                return View("AddPostForm"); 
            }
            if (ModelState.IsValid)
            {
                if (post.ID == 0)
                {
                    post.ID = posts.Count + 1;      //ახალი პოსტის ID მნიშვნელობა
                    post.Date = DateTime.Now.Date;  //შექმნის დრო
                    posts.Add(post);    //ლისტში დამატება.
                }
                else
                {
                    posts[post.ID - 1].Description = post.Description;      //არსებული პოსტის რედაქტირება.
                    posts[post.ID - 1].Title = post.Title;                  //
                    posts[post.ID - 1].ImageURL = post.ImageURL;            //
                }

                return RedirectToAction("Posts");           //Post Action-გამოძახება.
            }
            else
            {
                return View("AddPostForm");
            }
        }


        [HttpGet]
        [Route("Post/FullPost/{id}")]
        public IActionResult FullPost(int id)
        {
            ViewBag.Model = posts[id-1];        //ობიექტი, რომელიც მოთხოვნილ ID-ზე დგას.
            return View("FullPost");
        }


        [HttpGet]
        [Route("Post/ChangePostForm/{id}")]
        public IActionResult ChangePost(int id)
        {
            ViewBag.Button = "რედაქტირება";             //რედაქტირების გამოძახება
            return View("AddPostForm",posts[id-1]);      //ფორმის გამოძახება და მოთხოვნილი ობიექტის მნიშვნელობები.
        }



        public List<PostModel> ReturnList()
        {
            return posts;   //აბრუნებს პოსტების ლისტს.
        }
    }
}