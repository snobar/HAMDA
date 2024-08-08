using HAMDA.Core.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HAMDA.Helpers
{
    [Authorize]
    public class HamdaWrapper
    {
        private readonly ClaimsPrincipal _user;

        private readonly IHttpContextAccessor _contextAccessor;

        private UserManager<IdentityUser> _userManager;

        private Models.ViewModels.CurrentUser _currentUser;

        public Models.ViewModels.CurrentUser CurrentUser
        {
            get
            {
                return _currentUser;
            }
        }

        public HamdaWrapper(ClaimsPrincipal user)
        {
            _user = user;
            _contextAccessor = new HttpContextAccessor();
            SetUserManager();
            SetCurrentUser();
        }

        public HamdaWrapper()
        {
            _contextAccessor = new HttpContextAccessor();
            SetUserManager();
            SetCurrentUser();
        }


        private void SetUserManager()
        {
            var userManager = _contextAccessor.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
            _userManager = userManager;
        }

        private void SetCurrentUser()
        {
            var Claims = _user ?? _contextAccessor.HttpContext.User;
            var user = _userManager.GetUserAsync(Claims).Result;
            _currentUser = new Models.ViewModels.CurrentUser
            {
                UserId = user.Id.ToAnyType<Guid>(),
                Email = user.Email,
                UserName = user.UserName,
            };
        }
    }
}
