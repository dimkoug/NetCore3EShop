using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShopProject.Data;
using ShopProject.Domain.Interfaces;
using ShopProject.Domain.Services;
using ShopProject.MVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.MVC
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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultContext")));
            services.AddRazorPages().AddNewtonsoftJson();
            services.AddMvc().AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });



            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddIdentity<IdentityUser, IdentityRole>()
    // services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    //options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            services.AddSession();
            services.AddHttpContextAccessor();

            // using Microsoft.AspNetCore.Identity.UI.Services;
            services.AddSingleton<IEmailSender, EmailSender>();
            //services.AddApplicationInsightsTelemetry();
            services.AddScoped<ShoppingCartHelper>(sp => ShoppingCartHelper.GetCart(sp));
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IBannerStatisticService, BannerStatisticService>();
            services.AddScoped<IBlogCategoryService, BlogCategoryService>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<IBlogPostCategoryService, BlogPostCategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventLocationService, EventLocationService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IOfferDetailService, OfferDetailService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPressReleaseService, PressReleaseService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IFeatureAttributeService, FeatureAttributeService>();
            services.AddScoped<IShopCategoryService, ShopCategoryService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IBlogCategoryService, BlogCategoryService>();
            services.AddScoped<IBlogPostTagService, BlogPostTagService>();
            services.AddScoped<IBlogPostMediaService, BlogPostMediaService>();
            services.AddScoped<IEventMediaService, EventMediaService>();
            services.AddScoped<IEventTagService, EventTagService>();
            services.AddScoped<IShopCategoryFeatureService, ShopCategoryFeatureService>();
            services.AddScoped<IProductShopCategoryService, ProductShopCategoryService>();
            services.AddScoped<IProductMediaService, ProductMediaService>();
            services.AddScoped<IProductTagService, ProductTagService>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
