using Foodly_new.Data;
using Foodly_new.Models.DomainModels;
using Foodly_new.Models.EfModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Foodly_new.Controllers
{
    public class BlogsController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
         UserIdentity userIdentity;

        public BlogsController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }

        EFContext c = new EFContext();

        public IActionResult Index(int page = 1,int pageSize=3)
        {
            PagedList<Review> model = new PagedList<Review>(c.Reviews, page, pageSize);
            return View("Index", model);
        }

        public IActionResult Blog(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                try
                {                    
                    var blogContext = c.Reviews.Find(id);
                    ViewData["blogContent"] = blogContext.Blog.ToString();
                    ViewData["BlogHeader"] = blogContext.Header;
                    ViewData["BlogPictureURL"] = blogContext.BannerImage;
                    ViewData["BlogPrice"] = blogContext.Price;
                    ViewData["BlogPublishDate"] = blogContext.PublishDate;
                    ViewData["BlogRestaurantName"] = blogContext.RestaurantName;
                    ViewData["BlogStar"] = blogContext.Star;
                    ViewData["BlogUser"] = 0;

                    return View();
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }

            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult WriteBlog()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult WriteBlog(string Header, string ShortCast, string restaurantName, double star, int price, string Blog)
        {
            Review blog = new Review();
            //image to BYTE
            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                var imageData = ms.ToArray();

                ms.Close();
                ms.Dispose();
                string imageBase64Data = Convert.ToBase64String(imageData);
                blog.BannerImage = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                break;
            }



            //DATABASE ADD
            blog.ShortCast = ShortCast;
            blog.Header = Header;
            blog.Price = price;
            blog.Star = star;
            blog.Blog = Blog;
            blog.Publish = true;
            blog.RestaurantName = restaurantName;
            blog.PublishDate = DateTime.Now;
            blog.UserID = _userManager.GetUserId(User);
            c.Reviews.Add(blog);
            c.SaveChanges();

            return View();
        }

    }
}
