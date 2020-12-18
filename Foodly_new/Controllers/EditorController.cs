using Foodly_new.Data;
using Foodly_new.Models.DomainModels;
using Foodly_new.Models.EfModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodly_new.Controllers
{
    //[Authorize(Roles ="Editor")]
    public class EditorController : Controller
    {
        private UserManager<UserIdentity> _userManager;
        EFContext c = new EFContext();

        public EditorController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(int page=1,int pageSize=6)
        {
            if(c.Reviews.Where(x => x.Publish == false&&x.IsDeleted==false).ToList().Count < 1)
            {
                ViewData["Model"] = "Buralarda Birşey yok";
            }
            PagedList<Review> model = new PagedList<Review>(c.Reviews.Where(x => x.Publish == false && x.IsDeleted == false), page, pageSize);

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

                    return View();
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        [HttpPost]
        public IActionResult Blog(string ReviewID,string Header)
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
                        return RedirectToAction(nameof(Index));
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

    }
}
