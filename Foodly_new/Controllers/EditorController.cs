using Foodly_new.Data;
using Foodly_new.Models.DomainModels;
using Foodly_new.Models.EfModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodly_new.Controllers
{
    [Authorize(Roles = "Editor,Admin")]
    public class EditorController : Controller
    {
        private UserManager<UserIdentity> _userManager;
        EFContext c = new EFContext();

        public EditorController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Blogs(int page = 1, int pageSize = 6)
        {
            if (c.Reviews.Where(x => x.Publish == false && x.IsDeleted == false).ToList().Count < 1)
            {
                ViewData["Model"] = "Buralarda bir şey yok.";
            }
            PagedList<Review> model = new PagedList<Review>(c.Reviews.Where(x => x.Publish == false && x.IsDeleted == false).OrderByDescending(x => x.Publish), page, pageSize);

            return View("Blogs", model);
        }
        public IActionResult Menus(int page = 1, int pageSize = 6)
        {
            if (c.Menus.Where(x => x.IsPublished == false && x.IsDeleted == false).ToList().Count < 1)
            {
                ViewData["Model"] = "Buralarda bir şey yok.";
            }
            PagedList<Menu> model = new PagedList<Menu>(c.Menus.Where(x => x.IsPublished == false && x.IsDeleted == false).OrderByDescending(x => x.PublishDate), page, pageSize);

            return View("Menus", model);
        }
        public IActionResult Restaurants(int page = 1, int pageSize = 6)
        {
            if (c.Restaurants.Where(x => x.IsAccepted == false).ToList().Count < 1)
            {
                ViewData["Model"] = "Buralarda bir şey yok.";
            }
            PagedList<Restaurant> model = new PagedList<Restaurant>(c.Restaurants.Where(x => x.IsAccepted == false).OrderByDescending(x => x.PublishDate), page, pageSize);

            return View("Restaurants", model);
        }
        public async Task<IActionResult> Blog(string id)
        {
            if (id == null || id == "")
            {
                return RedirectToAction(nameof(Blogs));
            }
            else
            {
                try
                {
                    var blogContext = c.Reviews.Where(x => x.IsDeleted == false && x.Publish == false && x.ReviewID == id).ToList();
                    foreach (var item in blogContext)
                    {
                        var se = await _userManager.FindByIdAsync(item.UserID);
                        ViewData["blogContent"] = item.Blog.ToString();
                        ViewData["BlogHeader"] = item.Header;
                        ViewData["BlogPictureURL"] = item.BannerImage;
                        ViewData["BlogPrice"] = item.Price;
                        ViewData["BlogPublishDate"] = item.PublishDate;
                        ViewData["BlogRestaurantName"] = item.RestaurantName;
                        ViewData["BlogStar"] = item.Star;
                        ViewData["BlogUser"] = se.UserName;
                        ViewData["PhotoProfile"] = se.Profilephoto;
                        ViewData["ShorCast"] = item.ShortCast;
                        ViewData["id"] = item.ReviewID;
                    }
                    return View();
                }
                catch
                {
                    return RedirectToAction(nameof(Blogs));
                }
            }
        }

        [HttpPost]
        public IActionResult Blog(string ReviewID, string Header)
        {
            try
            {
                if (Header == "Publish")
                {
                    if (ReviewID != null)
                    {
                        List<Review> r = new List<Review>();
                        r = c.Reviews.Where(x => x.ReviewID == ReviewID && x.IsDeleted == false).ToList<Review>();
                        foreach (var item in r)
                        {
                            item.Publish = true;
                            item.IsDeleted = false;
                            c.Update(item);
                            c.SaveChanges();
                        }
                        return RedirectToAction(nameof(Blogs));
                    }
                    else
                    {
                        ViewData["Error"] = "Bir hata oluştu #3303";
                        return View();
                    }
                }
                else if (Header == "Delete")
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
                        return RedirectToAction(nameof(Blogs));
                    }
                    else
                    {
                        ViewData["Error"] = "Bir hata oluştu #3303";
                        return View();
                    }
                }
                else
                {
                    ViewData["Error"] = "Bir hata oluştu #3303";
                    return View();
                }
            }
            catch
            {
                ViewData["Error"] = "Bir hata oluştu #3303";
                return View();
            }

        }
        [HttpGet]
        public IActionResult Menu(string id)
        {
            if (id == null || id == "")
            {
                return RedirectToAction(nameof(Blogs));
            }
            else
            {
                try
                {
                    var blogContext = c.Menus.Where(x => x.IsDeleted == false && x.IsPublished == false && x.MenuID == id).ToList();
                    Menu menu = new Menu();
                    foreach (var item in blogContext)
                    {
                        menu.MenuID = item.MenuID;
                        menu.MenuHeader = item.MenuHeader;
                        menu.IsDeleted = false;
                        menu.IsPublished = false;
                        menu.PhotoDate = item.PhotoDate;
                        menu.PublishDate = item.PublishDate;
                        menu.RestaurantName = item.RestaurantName;
                        menu.UserID = item.UserID;
                    }
                    return View(menu);
                }
                catch
                {
                    return RedirectToAction(nameof(Blogs));
                }
            }
        }


        [HttpPost]
        public IActionResult Menu(string ReviewID, string Header)
        {
            try
            {
                if (Header == "Publish")
                {
                    if (ReviewID != null)
                    {
                        List<Menu> r = new List<Menu>();
                        r = c.Menus.Where(x => x.MenuID == ReviewID && x.IsDeleted == false).ToList<Menu>();
                        foreach (var item in r)
                        {
                            item.IsPublished = true;
                            item.IsDeleted = false;
                            c.Update(item);
                            c.SaveChanges();
                        }
                        return RedirectToAction(nameof(Blogs));
                    }
                    else
                    {
                        ViewData["Error"] = "Bir hata oluştu #3303";
                        return View();
                    }
                }
                else if (Header == "Delete")
                {
                    if (ReviewID != null)
                    {
                        List<Menu> r = new List<Menu>();
                        r = c.Menus.Where(x => x.MenuID == ReviewID && x.IsDeleted == false).ToList<Menu>();
                        foreach (var item in r)
                        {
                            item.IsPublished = false;
                            item.IsDeleted = true;
                            c.Update(item);
                            c.SaveChanges();
                        }
                        return RedirectToAction(nameof(Blogs));
                    }
                    else
                    {
                        ViewData["Error"] = "Bir hata oluştu #3303";
                        return View();
                    }
                }
                else
                {
                    ViewData["Error"] = "Bir hata oluştu #3303";
                    return View();
                }
            }
            catch
            {
                ViewData["Error"] = "Bir hata oluştu #3303";
                return View();
            }

        }

        [HttpGet]
        public IActionResult Restaurant(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                try
                {
                    var restaurantContext = c.Restaurants.Where(x => x.RestaurantID == id && x.IsAccepted == false&&x.Deleted==false);
                    return View(restaurantContext);
                }
                catch
                {
                    return Redirect("/Editor/restaurants");
                }
            }
        }
        [HttpPost]
        public IActionResult Restaurant(string id, string Action)
        {
            try
            {
                var Restaurant = c.Restaurants.Where(x => x.RestaurantID == id).ToList();
                if (Restaurant != null)
                {
                    if (Action == "Delete")
                    {
                        foreach (var item in Restaurant)
                        {
                            item.IsAccepted = false;
                            item.Deleted = true;
                            c.Update(item);
                        }
                    }
                    else if (Action == "Accept")
                    {
                        foreach (var item in Restaurant)
                        {
                            item.IsAccepted = true;
                            item.Deleted = false;
                            c.Update(item);
                        }
                    }
                    return Redirect("/Editor/restaurants");
                }
                else
                {
                    ViewData["Error"] = "Restoran bulunamadı #R2002";
                    return View();
                }
            }catch
            {
                ViewData["Error"] = "Bir hata oluştu #R3003";
                return View();
            }
        }

    }
}
