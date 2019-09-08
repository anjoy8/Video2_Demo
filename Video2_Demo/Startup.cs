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

            #region Automapper
            services.AddAutoMapper(typeof(Startup));
            #endregion


            #region JWT
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
            #endregion


            #region CORS
            //跨域第二种方法，声明策略，记得下边app中配置
            services.AddCors(c =>
            {

                //一般采用这种方法
                c.AddPolicy("LimitRequests", policy =>
                {
                    // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    // 注意，http://127.0.0.1:5401 和 http://localhost:5401 是不一样的，尽量写两个
                    policy
                    .WithOrigins("http://127.0.0.1:5401","http://localhost:5401", "http://127.0.0.1:5402", "http://localhost:5402")
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });

            //跨域第一种办法，注意下边 Configure 中进行配置
            //services.AddCors();
            #endregion
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


            #region CORS
            //跨域第二种方法，使用策略，详细策略信息在ConfigureService中
            app.UseCors("LimitRequests");//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。


            #region 跨域第一种版本
            //跨域第一种版本，请要ConfigureService中配置服务 services.AddCors();
            //    app.UseCors(options => options.WithOrigins("http://localhost:5401").AllowAnyHeader()
            //.AllowAnyMethod());  
            #endregion

            #endregion


            //app.UseMiddleware<JwtTokenAuth>();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
