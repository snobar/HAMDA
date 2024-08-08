using Microsoft.AspNetCore.Http;

namespace HAMDA.Provider.Middleware
{
    public class BlockIdentityMiddleware
    {
        private readonly RequestDelegate _next;

        public BlockIdentityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/Identity"))
            {
                context.Response.Redirect("/Home/Index");
                return;
            }

            await _next(context);
        }
    }

}
