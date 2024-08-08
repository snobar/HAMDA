using Microsoft.AspNetCore.Http;

namespace HAMDA.Provider.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorMessage = "Something went wrong! Please try again.";
                var redirectUrl = $"/Home/Index?errorMessage={Uri.EscapeDataString(errorMessage)}";
                context.Response.Redirect(redirectUrl);
            }
        }
    }
}
