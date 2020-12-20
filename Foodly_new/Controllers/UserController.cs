using Foodly_new.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodly_new.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        public UserController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View();
        }
    }
}
