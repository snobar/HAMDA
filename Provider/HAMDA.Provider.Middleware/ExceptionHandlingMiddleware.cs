using Microsoft.AspNetCore.Http;

namespace HAMDA.Provider.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly FileLoggerService _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, FileLoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorMessage = "somethingWentWrong";
                var redirectUrl = $"/Home/Index?errorMessage={Uri.EscapeDataString(errorMessage)}";

                await _logger.LogErrorAsync(ex.ToString());

                context.Response.Redirect(redirectUrl);
            }

            if (context.Response.StatusCode == StatusCodes.Status405MethodNotAllowed)
            {
                //var errorMessage = "somethingWentWrong";
                //var redirectUrl = $"/Home/Index?errorMessage={Uri.EscapeDataString(errorMessage)}";

                context.Response.Redirect("/Home/Index");
            }
        }
    }
}
