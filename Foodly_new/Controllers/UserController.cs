using Foodly_new.Data;
using Foodly_new.Models.DomainModels;
using Foodly_new.Models.EfModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodly_new.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        EFContext c = new EFContext();
        public UserController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;           
        }
        public async Task<IActionResult> Index()
        {
           var user= await _userManager.GetUserAsync(User);
            return RedirectToPage("/profile/", user.Id);
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string id, int page = 1, int pageSize = 6)
        {
            var user = await _userManager.FindByIdAsync(id);
            PagedList<Review> model = new PagedList<Review>(c.Reviews.Where(x => x.Publish == true && x.IsDeleted == false && x.UserID==id), page, pageSize);
            ViewData["Username"] = user.UserName;
            ViewData["profilephoto"] = user.Profilephoto;
            return View("profile", model);
        }
    }
}
