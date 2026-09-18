using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using BolosDoJacquin.Exceptions;

namespace BolosDoJacquin.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                if (ex is ConflictException)
                {
                    context.Response.StatusCode = 409;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new { erro = ex.Message }));
                    return;
                }
                if (ex is NotFoundException)
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new { erro = ex.Message }));
                    return;
                }
                
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { erro = "Erro interno no servidor." }));
            }
        }
    }
}
