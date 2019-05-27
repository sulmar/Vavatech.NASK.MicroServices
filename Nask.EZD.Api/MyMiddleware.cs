using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Nask.EZD.Api
{

    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyMiddleware>();
        }
    }

    public class MyMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMessagesService messagesService;

        // public MyMiddleware(RequestDelegate next)
        // : this(next, new FakeMessagesService())
        // {
            
        // }
        public MyMiddleware(RequestDelegate next, IMessagesService messagesService)
        {
            this.next = next;

            this.messagesService = messagesService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("MyMiddleware", "executed");

            messagesService.Send("Hello MyMiddleware");

            await next.Invoke(context);
        }

    }
}