using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Music3_Api.ApiResponse;

namespace Music3_Api.Filter
{
    /// <summary>
    /// custom reponse message to validator by flutent
    /// </summary>
    public class CustomValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();
                var responseObj = new ApiResponseResult
                {
                    Code = "200",
                    Message = "Đã xảy ra lỗi với dữ liệu đầu vào !",
                    Errors = new Dictionary<string, IEnumerable<string>>()
                    {
                        {
                            "msg",
                            errors
                        }
                    },
                    Success = false
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 200
                };
            }
        }
    }
}
