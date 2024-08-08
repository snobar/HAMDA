using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HAMDA.Controllers
{
    [Authorize]
    public class AuthorizedController : BaseController
    {
        protected Models.ViewModels.CurrentUser CurrentUser
        {
            get
            {
                return new HAMDA.Helpers.HamdaWrapper(User).CurrentUser;
            }
        }
    }
}
