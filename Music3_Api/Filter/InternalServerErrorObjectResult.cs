using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Music3_Api.Filter
{
    //respone return error
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
