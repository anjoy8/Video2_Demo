using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.AuthHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Video2_Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(Startup));


            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var symmetricKeyAsBase64 = "laozhanglaozhang";
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:5000",//发行人
                ValidateAudience = true,
                ValidAudience = "http://localhost:5001",//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            services.AddAuthentication("Bearer")
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            #region 中间件执行顺序

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("1、start");
            //    await next();
            //    Console.WriteLine("1、end");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("2、start");
            //    await next();
            //    Console.WriteLine("2、end");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("3、start");
            //    await next();
            //    Console.WriteLine("3、end");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("4、start");
            //    await next();
            //    Console.WriteLine("4、end");
            //}); 
            #endregion

            app.UseMiddleware<JwtTokenAuth>();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
