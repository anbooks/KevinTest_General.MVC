using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Core;
using General.Core.Data;
using General.Core.Extensions;
using General.Core.Librs;
using General.Entities;
using General.Framework;
using General.Framework.Infrastructure;
using General.Framework.Security.Admin;
using General.Services.Category;
using General.Services.Setting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KevinTest_General.MVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddDbContextPool<GeneralDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //一个实例，保证过个实例互相不干扰
            //services.AddDbContext<GeneralDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //真乱又让用pool了？
            services.AddDbContextPool<GeneralDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //#Kevin 添加权限过滤
            //services.AddAuthentication();
            services.AddAuthentication("General").AddCookie(o=>
            {
                o.LoginPath = "/Admin/Login/Index";
            });

            //单个的注入可以注释掉了
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<ISettingService, SettingService>();  //加一个表就得加一个，这要是很多可咋办

            //这个写在了extension中
            // var assembly=RuntimeHelper.GetAssemblyByName("General.Services");

            //var types = assembly.GetTypes();
            //var list = types.Where(o => o.IsClass && !o.IsAbstract && !o.IsGenericType).ToList();

            //foreach(var type in list)
            //{
            //    var interfacesList = type.GetInterfaces();
            //    if (interfacesList.Any())
            //    {
            //        var inter = interfacesList.First();
            //        services.AddScoped(inter, type);    //注入了后再homecongtroller中使用了
            //    }
            //}

            //程序集依赖注入
            services.AddAssembly("General.Services");


            //泛型注入到Di里面
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>) );

            services.AddScoped<IWorkContext, WorkContext>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //services.BuildServiceProvider().GetService<ICategoryService>();

            //#Kevin 引入引擎机制
            EnginContext.Initialize(new GeneralEngine(services.BuildServiceProvider()));
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();


            //Kevin 添加权限过滤
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  //template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  template: "{area:exists}/{controller=Login}/{action=Index}/{id?}"
                );
            });

        }
    }
}
