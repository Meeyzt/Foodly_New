using Foodly_new.Data;
using Foodly_new.Models.DomainModels;
using Foodly_new.Models.EfModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Foodly_new.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        EFContext c = new EFContext();

        public RestaurantController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(string Name, string Type, string Tel, string Web, string Adress)
        {
            if (Name != null && Type != null && Tel != null && Adress != null)
            {
                try
                {
                    c.Restaurants.Add(
                         new Restaurant
                         {
                             Name = Name,
                             Type = Type,
                             Web = Web,
                             Tel = Tel,
                             Adress = Adress,
                             RestaurantID = Guid.NewGuid().ToString(),
                             StarCount = 0,
                             CreatedByID = _userManager.GetUserId(User),
                             IsAccepted = false
                         });
                    c.SaveChanges();
                    return View();
                }
                catch
               {
                    ViewData["Error"] = "Bir şeyler ters gitti!. #30001";
                    return View();
                }
            }
            else
            {
                ViewData["Error"] = "Boş gönderemezsin #NULLERROR";
                return View();
            }
        }
    }
}
