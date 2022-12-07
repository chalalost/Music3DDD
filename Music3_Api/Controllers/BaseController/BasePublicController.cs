using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Music3_Api.Controllers.BaseController
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BasePublicController : ControllerBase
    {
    }
}
