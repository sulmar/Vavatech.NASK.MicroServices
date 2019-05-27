using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nask.EZD.Api
{
    public class Startup
    {

        public IConfiguration Configuration {get ; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddXmlFile("appsettings.xml", optional: true)
                
                ;

             Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessagesService, FakeMessagesService>();
  
            services.AddOptions();

            services.Configure<MyOptions>(Configuration.GetSection("MyMiddleware"));

            var myOptions = new MyOptions();
            Configuration.GetSection("MyMiddleware").Bind(myOptions);
            services.AddSingleton(myOptions);


            // Grace

        }

        private void MyHandler(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Hello Customers"));
        }

        private void MyHandlerActiveDocuments(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Active documents"));   
        }

        
        private void MyHandlerArchiveDocuments(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Archive documents"));   
        }

         private void MyHandlerQueryFormat(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Query format"));   
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env)
        {
            // TODO:

            Configure(app, env);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            var myOption1 = Configuration["MyOption1"];

            var protocol = Configuration["MyModule1:Protocol"];

            var connectionString = Configuration.GetConnectionString("MyConnection");

            if (env.IsEnvironment("Testing"))
            {

            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


          //  app.UseMiddleware<MyMiddleware>();

          app.UseMyMiddleware();


            app.Map("/customers", MyHandler);

            app.Map("/documents", path => 
            {
                path.Map("/active", MyHandlerActiveDocuments);
                path.Map("/archive", MyHandlerArchiveDocuments);

                path.Run(async context => await context.Response.WriteAsync("All documents"));
            });

            app.MapWhen(context => context.Request.Query.ContainsKey("format"), MyHandlerQueryFormat);

            app.Use(async (context, next) => 
            {
                context.Response.Headers.Add("Version", "1.0");

                await next.Invoke();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello .NET Core!!!");   
            });
        }
    }
}
