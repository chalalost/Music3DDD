using System;
using System.Collections.Generic;

namespace Music3_Api.ApiResponse
{
    [Serializable]
    public class ApiResponseResult
    {
        public bool Success { get; set; }

        public string Code { get; set; }

        public int HttpStatusCode { get; set; }

        public string Tittle { get; set; }

        public string Message { get; set; }

        public dynamic Data { get; set; }

        public int TotalCount { get; set; }

        public bool IsRedirect { get; set; }

        public string RedirectUrl { get; set; }

        public Dictionary<string, IEnumerable<string>> Errors { get; set; }

        public ApiResponseResult()
        {
            this.Success = true;
            this.HttpStatusCode = 200;
            this.Errors = new Dictionary<string, IEnumerable<string>>();
            this.IsRedirect = false;
        }

        public ApiResponseResult(ApiResponseResult obj)
        {
            this.Success = obj.Success;
            this.Code = obj.Code;
            this.HttpStatusCode = obj.HttpStatusCode;
            this.Tittle = obj.Tittle;
            this.Message = obj.Message;
            this.Data = obj.Data;
            this.TotalCount = obj.TotalCount;
            this.IsRedirect = obj.IsRedirect;
            this.RedirectUrl = obj.RedirectUrl;
            this.Errors = obj.Errors;
        }
    }
}
