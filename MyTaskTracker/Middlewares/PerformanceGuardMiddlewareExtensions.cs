namespace MyTaskTracker.Middlewares;

public static class PerformanceGuardMiddlewareExtensions
{
    public static IApplicationBuilder UsePerfomaceGuard(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PerformanceGuardMiddleware>();
    }
}