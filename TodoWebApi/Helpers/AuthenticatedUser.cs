using Microsoft.AspNetCore.Mvc;

namespace TodoWebApi.Helpers
{
    public class AuthenticatedUser :  ControllerBase
    {
        public string GetUserId() 
        {
            return HttpContext.User.Claims.Where(x => x.Type == "jti").FirstOrDefault().Value;
        }
    }
}
