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
        public IActionResult AddMenu(string MenuHeader, string RestaurantName, string Date)
        {
            Menu menu = new Menu();
            List<string> imageBase64Datas = new List<string>();

            //ImageToByte
            foreach (var file in Request.Form.Files)
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    var imageData = ms.ToArray();

                    ms.Close();
                    ms.Dispose();
                    string imageBase64Data = Convert.ToBase64String(imageData);
                    imageBase64Datas.Add(imageBase64Data);
                }
                catch
                {
                    //resim eklenmemiş
                }
            }
            //Add Database
            menu.MenuID = Guid.NewGuid().ToString();
            menu.IsDeleted = false;
            menu.IsPublished = false;
            menu.MenuHeader = MenuHeader;
            menu.Photos = imageBase64Datas.ToArray();
            menu.PublishDate = DateTime.Now;
            menu.Date = Date;
            menu.RestaurantName = RestaurantName;
            menu.UserID = _userManager.GetUserId(User);

            return View();
        }

    }
}
