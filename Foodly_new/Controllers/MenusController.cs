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
    public class MenusController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        EFContext c = new EFContext();
        public MenusController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            PagedList<Menu> model = new PagedList<Menu>(c.Menus.Where(x => x.IsPublished == true && x.IsDeleted == false), page, pageSize);
            return View("Index", model);
        }

        public async Task<IActionResult> Menu(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                try
                {
                    var menuContext = c.Menus.Find(id);
                    var se = await _userManager.FindByIdAsync(menuContext.UserID);

                    ViewData["UserId"] = menuContext.UserID;

                    var commentcontext = c.Comments.Where(x => x.ReviewID == menuContext.MenuID).ToList();

                    return View(commentcontext);
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        [HttpPost]
        public IActionResult Menu(string MenuID, string Header)
        {
            try
            {
                if (Header == "Publish")
                {
                    if (MenuID != null)
                    {
                        List<Menu> r = new List<Menu>();
                        r = c.Menus.Where(x => x.MenuID == MenuID && x.IsPublished == false).ToList();
                        foreach (var item in r)
                        {
                            item.IsPublished = true;
                            item.IsDeleted = false;
                            c.Update(item);
                            c.SaveChanges();
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["Error"] = "Bir hata oluştu #4402";
                        return View();
                    }
                }
                else if (Header == "Delete")
                {
                    if (MenuID != null)
                    {
                        List<Menu> r = new List<Menu>();
                        r = c.Menus.Where(x => x.MenuID == MenuID && x.IsDeleted == false).ToList();
                        foreach (var item in r)
                        {
                            item.IsPublished = false;
                            item.IsDeleted = true;
                            c.Update(item);
                            c.SaveChanges();
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["Error"] = "Bir hata oluştu #4403";
                        return View();
                    }
                }
                else
                {
                    ViewData["Error"] = "Bir hata oluştu #4404";
                    return View();
                }
            }
            catch
            {
                ViewData["Error"] = "Bir hata oluştu #4405";
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddMenu()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddMenu(string MenuHeader, string RestaurantName, string PhotoDate)
        {
            Menu menu = new Menu();
            MenuPhotos mPhotos = new MenuPhotos();
            List<string> imageBase64Datas = new List<string>();
            menu.MenuID = Guid.NewGuid().ToString();

            //ImageToByteToString
            foreach (var file in Request.Form.Files)
            {
                try
                {

                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    var imageData = ms.ToArray();
                    ms.Close();
                    ms.Dispose();

                    mPhotos.PhotoID= Guid.NewGuid().ToString();

                    string imageBase64Data = Convert.ToBase64String(imageData);
                    mPhotos.MenuID = menu.MenuID;
                    string photos = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                    mPhotos.Photo = photos;
                    c.MenuPhotos.Add(mPhotos);
                    c.SaveChanges();
                }
                catch
                {
                    //Not add image
                }
            }

            //Add Database            
            menu.IsDeleted = false;
            menu.IsPublished = false;
            menu.MenuHeader = MenuHeader;
            menu.PublishDate = DateTime.Now;
            menu.PhotoDate = PhotoDate;
            menu.RestaurantName = RestaurantName;
            menu.UserID = _userManager.GetUserId(User);
            c.Menus.Add(menu);
            c.SaveChanges();

            return View();
        }
    }
}
