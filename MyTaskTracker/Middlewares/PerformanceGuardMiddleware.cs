using System.Diagnostics;

namespace MyTaskTracker.Middlewares;

public class PerformanceGuardMiddleware
{
    private readonly RequestDelegate _next;
    
    public  PerformanceGuardMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<PerformanceGuardMiddleware> logger)
    {
        var sw = Stopwatch.StartNew();

        context.Response.OnStarting(() =>
        {
            sw.Stop();
            context.Response.Headers.Append("X-Response-Time-ms", $"{sw.ElapsedMilliseconds}ms");
            return Task.CompletedTask;
        });
        
        await _next(context);

        logger.LogInformation("[{Method}] {Path} обработан за {Elapsed} мс (Status: {StatusCode})",
            context.Request.Method,
            context.Request.Path,
            sw.ElapsedMilliseconds,
            context.Response.StatusCode);

    } 
}