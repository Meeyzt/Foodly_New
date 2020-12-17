using Foodly_new.Data;
using Foodly_new.Models.DomainModels;
using Foodly_new.Models.EfModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Foodly_new.Controllers
{
    public class BlogsController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
         List<UserIdentity> userIdentity;
        EFContext c = new EFContext();

        public BlogsController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }

        EFContext c = new EFContext();

        public IActionResult Index(int? id)
        {
            List<Review> x = c.Reviews.Where(i => i.Publish == true).ToList();
            List<Review> y = new List<Review>();
            UserIdentity user = new UserIdentity();
            try
            {
                if (id == null || id == 0 || id == 1)
                {
                    if (x.Count <= 6)
                    {
                        y.Clear();
                        for (int i = 0; i < x.Count; i++)
                        {
                       
                        }
                    }
                    else
                    {
                        y.Clear();
                        for (int i = 0; i < 6; i++)
                        {
                           
                        }
                    }

                    return View(y);
                }
                else
                {
                    y.Clear();
                    if (id >= 2)
                    {
                        int count = (int)(id * 6);
                        int start = count - 6;

                        if (count >= x.Count())
                        {
                            for (int i = start; i < x.Count(); i++)
                            {
                                
                            }
                        }
                        else
                        {
                            for (int i = start; i <= count; i++)
                            {
                                
                            }
                        }
                        return View(y);
                    }
                    else
                    {
                        return View(nameof(Index));
                    }


                }
            }
            catch (Exception e)
            {
                y.Clear();
                for (int i = 0; i <= 6; i++)
                {
                    
                }
                return View(y);
            }
        public IActionResult Index(int page = 1,int pageSize=6)
        {
            PagedList<Review> model = new PagedList<Review>(c.Reviews.Where(x=>x.Publish==true&&x.IsDeleted==false), page, pageSize);
            return View("Index", model);
        }

        public async Task<IActionResult> Blog(int ?id)
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
                    //username ekleenecek
                    var se = await _userManager.FindByIdAsync(blogContext.UserID);
                    ViewData["blogContent"] = blogContext.Blog.ToString();
                    ViewData["BlogHeader"] = blogContext.Header;
                    ViewData["BlogPictureURL"] = blogContext.BannerImage;
                    ViewData["BlogPrice"] = blogContext.Price;
                    ViewData["BlogPublishDate"] = blogContext.PublishDate;
                    ViewData["BlogRestaurantName"] = blogContext.RestaurantName;
                    ViewData["BlogStar"] = blogContext.Star;
                    ViewData["BlogUser"] = 0;
                    ViewData["BlogUser"] = se.UserName;
                    ViewData["PhotoProfile"] = se.Profilephoto;

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
            blog.ReviewID = Guid.NewGuid().ToString();
            blog.ShortCast = ShortCast;
            blog.Header = Header;
            blog.Price = price;
            blog.Star = star;
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

    }
}
