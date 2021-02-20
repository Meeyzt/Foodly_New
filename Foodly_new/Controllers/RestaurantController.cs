using Foodly_new.Data;
using Foodly_new.Models.DomainModels;
using Foodly_new.Models.EfModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(string Name, string Type, string Tel, string Web, string Adress,string District, string City)
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
                             District = District,
                             City = City,
                             Adress = Adress,
                             RestaurantID = Guid.NewGuid().ToString(),
                             StarCount = 0,
                             CreatedByID = _userManager.GetUserId(User),
                             PublishDate = DateTime.Now,
                             IsAccepted = false,
                             Deleted=false
                         }) ;
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

        [HttpPost]
        public JsonResult IlIlce(int? ilID, string tip)
        {
            //geriye döndüreceğim sonucListim
            List<SelectListItem> sonuc = new List<SelectListItem>();
            //bu işlem başarılı bir şekilde gerçekleşti mi onun kontrolunnü yapıyorum
            bool basariliMi = true;
            try
            {
                switch (tip)
                {
                    case "ilGetir":
                        //veritabanımızdaki iller tablomuzdan illerimizi sonuc değişkenimze atıyoruz
                        foreach (var il in c.Iller.ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = il.Il,
                                Value = il.IlID.ToString()
                            });
                        }
                        break;
                    case "ilceGetir":
                        //ilcelerimizi getireceğiz ilimizi selecten seçilen ilID sine göre 
                        foreach (var ilce in c.Ilceler.Where(il => il.IlID == ilID).ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = ilce.Ilce,
                                Value = ilce.IlceID.ToString()
                            });
                        }
                        break;

                    default:
                        break;

                }
            }
            catch (Exception)
            {
                //hata ile karşılaşırsak buraya düşüyor
                basariliMi = false;
                sonuc = new List<SelectListItem>();
                sonuc.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });

            }
            //Oluşturduğum sonucları json olarak geriye gönderiyorum
            return Json(new { ok = basariliMi, text = sonuc });
        }
        [HttpGet]
        public IActionResult Profile(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                try
                {
                    var restaurantContext = c.Restaurants.Where(x=> x.RestaurantID==id && x.IsAccepted==true&& x.Deleted==false);
                    return View(restaurantContext);
                }
                catch
                {
                    return Redirect("/Home/Index");
                }
            }
        }
        [HttpPost]
        public IActionResult Profile(string id, string Action)
        {
            if(id!=null && id!=""&& Action!=null && Action!="" )
            {
                if (Action == "Delete")
                {
                    var list = c.Restaurants.Where(x => x.RestaurantID == id).ToList();
                    foreach (var item in list)
                    {
                        item.IsAccepted = false;
                        item.Deleted = true;
                        c.Restaurants.Update(item);
                    }
                    c.SaveChanges();
                }
            }
            return View();
        }
    }
}
