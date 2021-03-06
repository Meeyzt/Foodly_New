﻿using Foodly_new.Data;
using Foodly_new.Models.DomainModels;
using Foodly_new.Models.EfModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Foodly_new.Controllers
{
    public class BlogsController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        EFContext c = new EFContext();

        public BlogsController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            PagedList<Review> model = new PagedList<Review>(c.Reviews.Where(x => x.Publish == true && x.IsDeleted == false).OrderByDescending(x => x.PublishDate), page, pageSize);
            return View("Index", model);
        }
        public async Task<IActionResult> Blog(string id)
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
                    var se = await _userManager.FindByIdAsync(blogContext.UserID);
                    ViewData["blogContent"] = blogContext.Blog.ToString();
                    ViewData["BlogHeader"] = blogContext.Header;
                    ViewData["BlogPictureURL"] = blogContext.BannerImage;
                    ViewData["BlogPrice"] = blogContext.Price;
                    ViewData["BlogPublishDate"] = blogContext.PublishDate;
                    ViewData["BlogRestaurantName"] = blogContext.RestaurantName;
                    ViewData["BlogStar"] = blogContext.Star;
                    ViewData["BlogUser"] = se.UserName;
                    ViewData["PhotoProfile"] = se.Profilephoto;
                    ViewData["ShorCast"] = blogContext.ShortCast;
                    ViewData["id"] = id;
                    ViewData["UserId"] = blogContext.UserID;
                    ViewData["RestaurantID"] = blogContext.RestaurantID;

                    var commentcontext = c.Comments.Where(x => x.ReviewID == blogContext.ReviewID && x.IsDeleted == false).ToList();
                    return View(commentcontext);
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
        public IActionResult WriteBlog(string Header, string ShortCast, string RestaurantID, string star, int price, string Blog, IFormFile file)
        {
            try
            {
                Review blog = new Review();
                //image to BYTE
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                var imageData = ms.ToArray();

                ms.Close();
                ms.Dispose();
                string imageBase64Data = Convert.ToBase64String(imageData);
                blog.BannerImage = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

                var restaurantname = c.Restaurants.Where(x => x.RestaurantID == RestaurantID).ToList();
                //DATABASE ADD
                blog.ReviewID = Guid.NewGuid().ToString();
                blog.ShortCast = ShortCast;
                blog.Header = Header;
                blog.Price = price;
                blog.Star = Convert.ToDouble(star);
                blog.Blog = Blog;
                blog.Publish = false;
                blog.IsDeleted = false;
                blog.RestaurantID = RestaurantID;
                blog.RestaurantName = restaurantname[0].Name;
                blog.PublishDate = DateTime.Now;
                blog.UserID = _userManager.GetUserId(User);
                c.Reviews.Add(blog);
                c.SaveChanges();

                return View();
            }
            catch
            {
                ViewData["Error"] = "Bir şeyler Ters gitti";
                return View();
            }
        }

        public async Task<IActionResult> AddComment(string id)
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
                    var se = await _userManager.FindByIdAsync(blogContext.UserID);
                    ViewData["BlogHeader"] = blogContext.Header;
                    ViewData["BlogPictureURL"] = blogContext.BannerImage;
                    ViewData["BlogPublishDate"] = blogContext.PublishDate;
                    ViewData["PhotoProfile"] = se.Profilephoto;
                    ViewData["ShortCast"] = blogContext.ShortCast;
                    ViewData["id"] = blogContext.ReviewID;
                    ViewData["BlogUser"] = se.UserName;
                    return View();
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddComment(string ReviewID, string Entry)
        {
            c.Comments.Add(new Comment
            {
                ReviewID = ReviewID,
                Entry = Entry,
                CommentID = Guid.NewGuid().ToString(),
                PublishDate = DateTime.Now,
                UserID = _userManager.GetUserId(User),
                IsDeleted = false
            });
            var s = c.SaveChanges();
            if (s > 0)
            {
                return RedirectToPage("/Blogs/Blog", ReviewID);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(string id, string type)
        {
            if (type == "Blog")
            {
                try
                {
                    if (id != null)
                    {
                        List<Review> r = new List<Review>();
                        r = c.Reviews.Where(x => x.ReviewID == id && x.IsDeleted == false).ToList();
                        foreach (var item in r)
                        {
                            item.Publish = false;
                            item.IsDeleted = true;
                            c.Update(item);
                        }
                        c.SaveChanges();
                        return Redirect("/Blogs");
                    }
                    else
                    {
                        ViewData["Error"] = "Bir hata oluştu #3303";
                        return Redirect("/Blogs");
                    }
                }
                catch
                {
                    ViewData["Error"] = "Bir hata oluştu #3305";
                    return Redirect("/Blogs");
                }
            }
            if (type == "Comment")
            {
                string reviewID = "";
                var x = c.Comments.Where(x => x.CommentID == id).ToList();
                foreach (var item in x)
                {
                    item.IsDeleted = true;
                    reviewID = item.ReviewID;
                    c.Comments.Update(item);
                }
                c.SaveChanges();
                return Redirect("/Blogs/Blog/" + reviewID);
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}
