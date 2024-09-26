namespace PersonnelManagement.Middlewares
{
    public class RoleMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity!.IsAuthenticated)
            {
                var roles = context.User.Claims
                                        .Where(c => c.Type == "role")
                                        .Select(c => c.Value)
                                        .ToList();

                if (roles.Contains("root"))
                {
                    // Quyền của root có thể truy cập mọi nơi
                    await _next(context);
                }
                else if (roles.Contains("admin"))
                {
                    // Quyền của admin, ví dụ: chỉ có thể truy cập các route admin
                    if (context.Request.Path.StartsWithSegments("/admin"))
                    {
                        await _next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Access Denied: Admins cannot access this resource.");
                        return;
                    }
                }
                else if (roles.Contains("user"))
                {
                    // Quyền của user, ví dụ: chỉ có thể truy cập các route user
                    if (context.Request.Path.StartsWithSegments("/user"))
                    {
                        await _next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Access Denied: Users cannot access this resource.");
                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Access Denied: No valid role.");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Access Denied: Unauthorized.");
                return;
            }
        }
    }

}
