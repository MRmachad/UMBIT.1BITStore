using System.Reflection.Metadata;

namespace BIT.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext);
            }
            catch(CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex);
            }
        }

        private void HandleRequestExceptionAsync(HttpContext httpContext, CustomHttpRequestException ex)
        {
           if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                httpContext.Response.Redirect("/login");
                return;
            }

            httpContext.Response.StatusCode = (int)ex.StatusCode;
        }

    }
}
