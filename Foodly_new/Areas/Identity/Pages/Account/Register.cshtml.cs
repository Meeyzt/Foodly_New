﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Foodly_new.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Foodly_new.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<UserIdentity> userManager,
            SignInManager<UserIdentity> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Eposta")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} {2} ile {1} karakter uzunluğunda olmalıdır.", MinimumLength = 8 )]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Tekrar şifre")]
            [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
            public string ConfirmPassword { get; set; }
            //I Added
            [Display(Name = "Kullanıcı Adı")]
            public string Username { get; set; }
        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var profilePhoto = "data:image/jpg;base64,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";
                var user = new UserIdentity { UserName = Input.Username, Email = Input.Email,Profilephoto= profilePhoto,EmailConfirmed=true };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Epostanı onayla",
                        $"Epostanı onaylamak için  <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Buraya tıkla</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("Login", new {returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
