namespace BeatsWave.Web.Middlewares
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    public class SetMaxRequestMiddleware
    {
        private readonly RequestDelegate next;

        public SetMaxRequestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = 100000000;

            await this.next(context);
        }
    }
}
