namespace Auth.Api.Middleware
{
    public class CsrfMiddleware
    {
        private readonly RequestDelegate _next;

        public CsrfMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;

            var isStateChanging =
                HttpMethods.IsPost(method) ||
                HttpMethods.IsPut(method) ||
                HttpMethods.IsDelete(method);

            if (!isStateChanging)
            {
                await _next(context);
                return;
            }

            if (path.StartsWithSegments("/api/auth/login") ||
               path.StartsWithSegments("/api/auth/register"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Cookies.ContainsKey("access_token"))
            {
                await _next(context);
                return;
            }

            var csrfCookie = context.Request.Cookies["csrf_token"];
            var csrfHeader = context.Request.Headers["X-CSRF-TOKEN"].ToString();

            if (string.IsNullOrEmpty(csrfCookie) ||
                string.IsNullOrEmpty(csrfHeader) ||
                csrfCookie != csrfHeader)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("CSRF validation failed");
                return;
            }

            await _next(context);
        }
    }

}
