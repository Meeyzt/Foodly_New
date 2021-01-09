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

        public IActionResult Index(int page = 1,int pageSize=6)
        {
            PagedList<Review> model = new PagedList<Review>(c.Reviews.Where(x=>x.Publish==true&&x.IsDeleted==false).OrderByDescending(x => x.PublishDate), page, pageSize);
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
                    ViewData["id"] = blogContext.ReviewID;
                    ViewData["UserId"] = blogContext.UserID;

                    var commentcontext = c.Comments.Where(x=>x.ReviewID==blogContext.ReviewID).ToList();

                    return View(commentcontext);
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        [HttpPost]
        public IActionResult Blog(string ReviewID, string Header)
        {
            try
            {
                if (Header == "Delete")
                {
                    if (ReviewID != null)
                    {
                        List<Review> r = new List<Review>();
                        r = c.Reviews.Where(x => x.ReviewID == ReviewID && x.IsDeleted == false).ToList<Review>();
                        foreach (var item in r)
                        {
                            item.Publish = false;
                            item.IsDeleted = true;
                            c.Update(item);
                            c.SaveChanges();
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["Error"] = "Bir hata oluştu #3303";
                        return View();
                    }
                }
                else
                {
                    ViewData["Error"] = "Bir hata oluştu #3304";
                    return View();
                }
            }
            catch
            {
                ViewData["Error"] = "Bir hata oluştu #3305";
                return View();
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
        public IActionResult WriteBlog(string Header, string ShortCast, string restaurantName, string star, int price, string Blog)
        {
            try
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
            blog.ReviewID = Guid.NewGuid().ToString();
            blog.ShortCast = ShortCast;
            blog.Header = Header;
            blog.Price = price;
            blog.Star = Convert.ToDouble(star);
            blog.Blog = Blog;
            blog.Publish = false;
            blog.IsDeleted = false;
            blog.RestaurantName = restaurantName;
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
        public IActionResult AddComment(string ReviewID,string Entry)
        {
            var comments = c.Comments.Add(new Comment
            {
                ReviewID = ReviewID,
                Entry = Entry,
                CommentID = Guid.NewGuid().ToString(),
                PublishDate = DateTime.Now,
                UserID = _userManager.GetUserId(User)
            }) ;
            var s = c.SaveChanges();
            if (s > 0)
            {
                return RedirectToPage("/Blogs/Blog",ReviewID);
            }
            return View();
        }
    }
}
