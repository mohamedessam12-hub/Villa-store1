using Microsoft.AspNetCore.Identity;

namespace MagicVila_VillaAPi.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
