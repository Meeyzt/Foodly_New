using Microsoft.AspNetCore.Identity;

namespace Foodly_new.Data
{
    public class UserIdentity:IdentityUser
    {
        public string Profilephoto { get; set; }
    }
}
