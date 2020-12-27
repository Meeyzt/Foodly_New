using Foodly_new.Models;
using Foodly_new.Models.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Foodly_new.Models.HomeModels;

namespace Foodly_new.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        EFContext c = new EFContext();

        public IActionResult Index()
        {
            var lx = c.Reviews.Where(x => x.Publish == true && x.IsDeleted == false).OrderByDescending(x => x.PublishDate).ToList();
            List<Values> ly = new List<Values>();
            int Timer = 0,Ender=0,count =0;
            if (lx.Count() < 5 && lx.Count > 0)
            {
                count = lx.Count-1;
            }else if(lx.Count()>=6)
            {
                count = 5;
            }
            for (int i = 0; i <= count && count!=0; i++)
            {
                if (Timer < 2)
                {
                    Timer += 1;
                    Ender += 1;
                    ly.Add(new Values
                    {
                        ReviewID=lx[i].ReviewID,
                        BannerImage = lx[i].BannerImage,
                        ShortCast = lx[i].ShortCast,
                        Star = lx[i].Star,
                        Header = lx[i].Header,
                        Price = lx[i].Price,
                        classLibrary = ""
                    });
                }
                else
                {
                    Ender += 1;
                    Timer += 1;
                    ly.Add(new Values
                    {
                        ReviewID = lx[i].ReviewID,
                        BannerImage = lx[i].BannerImage,
                        ShortCast = lx[i].ShortCast,
                        Star = lx[i].Star,
                        Header = lx[i].Header,
                        Price = lx[i].Price,
                        classLibrary = "order-md-last"
                    });
                    if (Timer == 4) Timer = 0;
                }
                if (Ender == 6)
                    break;
            }
            return View(ly);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
