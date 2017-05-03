namespace HappyMe.Web.Common.Extensions
{
    using System.Net;
    using System.Web;

    public static class HttpResponseBaseExtensions
    {
        public static bool IsError(this HttpResponseBase response)
        {
            return response.StatusCode >= (int)HttpStatusCode.BadRequest;
        }
    }
}
