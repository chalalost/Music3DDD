using MediatR;
using Microsoft.AspNetCore.Mvc;
using Music3_Api.ApiResponse;
using Music3_Api.Applications.RedisCache.RedisCacheExtension;
using Music3_Api.Commands.Paging.Slide;
using Music3_Api.Controllers.BaseController;
using System.Net;
using System.Threading.Tasks;

namespace Music3_Api.Controllers
{
    public class SlideController : BasePublicController
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;

        public SlideController(IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat;
            _cacheExtension = cacheExtension;
        }

        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] SlidePaginatedCmd paginatedList)
        {
            var data = await _mediat.Send(paginatedList);
            var result = new ApiResponseResult()
            {
                Data = data.Result,
                Success = true,
                TotalCount = data.TotalCount
            };
            return Ok(result);
        }
    }
}
