using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using ShopProject3.DataAccess;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Services;
using ShopProject3.Helpers;

namespace ShopProject3.MVC
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
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

            // using Microsoft.AspNetCore.Identity.UI.Services;
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<ShoppingCartHelper>(sp => ShoppingCartHelper.GetCart(sp));

            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IBrandsService, BrandsService>();
            services.AddScoped<ITagsService, TagsService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IFeaturesService, FeatureService>();
            services.AddScoped<IFeatureAttributesService, FeatureAttributesService>();
            services.AddScoped<ICategoryFeaturesService, CategoryFeaturesService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IProductCategoriesService, ProductCategoriesService>();
            services.AddScoped<IProductTagsService, ProductTagsService>();
            services.AddScoped<IProductMediaService, ProductMediaService>();
            services.AddScoped<IProductFeatureAttributesService, ProductFeatureAttributesService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IOrdersDetailService, OrdersDetailService>();
            services.AddScoped<IPressReleasesService, PressReleasesService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<IBannersService, BannersService>();
            services.AddScoped<IOffersService, OffersService>();
            services.AddScoped<IEventsMediaService, EventsMediaService>();
            services.AddScoped<IOffersDetailService, OffersDetailService>();
            services.AddApplicationInsightsTelemetry();
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
