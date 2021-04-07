
using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using S3.Common;
using S3.Common.Authentication;
using S3.Common.Consul;
using S3.Common.Dispatchers;
using S3.Common.Jaeger;
using S3.Common.Mvc;
using S3.Common.RabbitMq;
using S3.Common.Redis;
using S3.Common.RestEase;
using S3.Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using CacheManager.core;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;
using OpenTracing;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace S3.ApiGateway
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };
        //public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"appsettings.ocelot.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddCustomMvc();
            services.AddSwaggerDocs();
            services.AddConsul();
            //services.AddJwt();
            //services.AddJaeger(); // Throwing exception
            services.AddOpenTracing();
            //services.AddRedis();
            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            //.AllowCredentials()
                            .WithExposedHeaders(Headers));
            });

            services.AddOcelot(Configuration);
            //.AddCacheManager();

            //var builder = new ContainerBuilder();
            //builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
            //        .AsImplementedInterfaces();
            //builder.Populate(services);
            //builder.AddRabbitMq();
            //builder.AddDispatchers();

            //Container = builder.Build();

            //return new AutofacServiceProvider(Container);
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            //builder.RegisterModule(new MyApplicationModule());

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.AddRabbitMq();
            builder.AddDispatchers();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime, IConsulClient client,
            IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            //app.UseAuthentication();
            //app.UseAccessTokenValidator();
            app.UseServiceId();
            app.UseMvc();
            await app.UseOcelot();
            app.UseRabbitMq();

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                //Container.Dispose();
            });

            await startupInitializer.InitializeAsync();
        }
    }
}
