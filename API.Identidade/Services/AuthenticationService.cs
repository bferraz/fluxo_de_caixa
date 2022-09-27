using API.Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Identidade.Services
{
    public class AuthenticationService
    {
        public readonly SignInManager<IdentityUser> SignInManager;
        public readonly UserManager<IdentityUser> UserManager;

        private readonly IAspNetUser _aspNetUser;

        public AuthenticationService(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, 
            IAspNetUser aspNetUser)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            _aspNetUser = aspNetUser;
        }
    }
}
