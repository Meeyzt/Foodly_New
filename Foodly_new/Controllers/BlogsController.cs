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

namespace Foodly_new.Controllers
{
    public class BlogsController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;

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
                            y.Add(new Review()
                            {
                                ReviewID = x[i].ReviewID,
                                RestaurantName = x[i].RestaurantName,
                                BannerImage = x[i].BannerImage,
                                Blog = x[i].Blog,
                                Header = x[i].Header,
                                ImageData = x[i].ImageData,
                                Price = x[i].Price,
                                Publish = x[i].Publish,
                                PublishDate = x[i].PublishDate,
                                Star = x[i].Star,
                                UserID = x[i].UserID
                            });
                        }
                    }
                    else
                    {
                        y.Clear();
                        for (int i = 0; i < 6; i++)
                        {
                            y.Add(new Review()
                            {
                                ReviewID = x[i].ReviewID,
                                RestaurantName = x[i].RestaurantName,
                                BannerImage = x[i].BannerImage,
                                Blog = x[i].Blog,
                                Header = x[i].Header,
                                ImageData = x[i].ImageData,
                                Price = x[i].Price,
                                Publish = x[i].Publish,
                                PublishDate = x[i].PublishDate,
                                Star = x[i].Star,
                                UserID = x[i].UserID
                            });
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
                                y.Add(new Review()
                                {
                                    ReviewID = x[i].ReviewID,
                                    RestaurantName = x[i].RestaurantName,
                                    BannerImage = x[i].BannerImage,
                                    Blog = x[i].Blog,
                                    Header = x[i].Header,
                                    ImageData = x[i].ImageData,
                                    Price = x[i].Price,
                                    Publish = x[i].Publish,
                                    PublishDate = x[i].PublishDate,
                                    Star = x[i].Star,
                                    UserID = x[i].UserID
                                });
                            }
                        }
                        else
                        {
                            for (int i = start; i <= count; i++)
                            {
                                y.Add(new Review()
                                {
                                    ReviewID = x[i].ReviewID,
                                    RestaurantName = x[i].RestaurantName,
                                    BannerImage = x[i].BannerImage,
                                    Blog = x[i].Blog,
                                    Header = x[i].Header,
                                    ImageData = x[i].ImageData,
                                    Price = x[i].Price,
                                    Publish = x[i].Publish,
                                    PublishDate = x[i].PublishDate,
                                    Star = x[i].Star,
                                    UserID = x[i].UserID
                                });
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
                    y.Add(new Review()
                    {
                        ReviewID = x[i].ReviewID,
                        RestaurantName = x[i].RestaurantName,
                        BannerImage = x[i].BannerImage,
                        Blog = x[i].Blog,
                        Header = x[i].Header,
                        ImageData = x[i].ImageData,
                        Price = x[i].Price,
                        Publish = x[i].Publish,
                        PublishDate = x[i].PublishDate,
                        Star = x[i].Star,
                        UserID = x[i].UserID
                    });
                }
                return View(y);
            }



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
                    ViewData["BlogUser"] = _userManager.GetUserNameAsync(blogContext.UserID);

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
        public IActionResult WriteBlog(string Header, string restaurantName, double star, int price, string Blog)
        {
            Review blog = new Review();
            //image to BYTE
            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                blog.ImageData = ms.ToArray();

                ms.Close();
                ms.Dispose();
                break;
            }

            string imageBase64Data = Convert.ToBase64String(blog.ImageData);
            string BannerImage = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            //DATABASE ADD
            blog.Header = Header;
            blog.Price = price;
            blog.Star = star;
            blog.Blog = Blog;
            blog.Publish = true;
            blog.BannerImage = BannerImage;
            blog.RestaurantName = restaurantName;
            blog.PublishDate = DateTime.Now;
            c.Reviews.Add(blog);
            c.SaveChanges();

            return View();
        }

    }
}
