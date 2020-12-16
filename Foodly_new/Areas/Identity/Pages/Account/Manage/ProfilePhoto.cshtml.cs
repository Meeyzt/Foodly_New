using Foodly_new.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace Foodly_new.Areas.Identity.Pages.Account.Manage
{
    public class ProfilePhotoModel :PageModel
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ProfilePhotoModel(
            UserManager<UserIdentity> userManager,
            SignInManager<UserIdentity> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Display(Name = "Profil fotoğrafı yükle")]
            public IFormFile ProfileImage { get; set; }
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kullanıcı Bulunamadı #'{_userManager.GetUserId(User)}'.\n Bu kodu destek birimimize bildirerek yardım alabilirsiniz.");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kullanıcı Bulunamadı #'{_userManager.GetUserId(User)}'.\n Bu kodu destek birimimize bildirerek yardım alabilirsiniz.");
            }
            //upload a photo
            if (Input.ProfileImage != null) { 
                MemoryStream ms = new MemoryStream();
                Input.ProfileImage.CopyTo(ms);
                var imageData = ms.ToArray();
            
                ms.Close();
                ms.Dispose();
                string imageBase64Data = Convert.ToBase64String(imageData);
                user.Profilephoto = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            }

            if (user.Profilephoto!=null && user.Profilephoto!= "")
            {
                var UpdateProfilePhoto = await _userManager.UpdateAsync(user);
                if (!UpdateProfilePhoto.Succeeded)
                {
                    foreach (var error in UpdateProfilePhoto.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }
            else
            {
                StatusMessage = "Bir Profil Fotoğrafı seçiniz";
                return Page();
            }

            

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their profile photo successfully.");
            StatusMessage = "Profil fotoğrafınız başarıyla değişti.";

            return RedirectToPage();
        }
    }
}
