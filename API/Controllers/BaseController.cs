using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Domain.RequestFeatures;
using Newtonsoft.Json;

namespace API.Controllers
{
    public abstract class BaseController: ControllerBase
    {
        protected string GetEmailFromHttpContextAsync()
        {
            return  HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        protected void SetResponseHeaders(MetaData metaData)
        {
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
        }
    }
}