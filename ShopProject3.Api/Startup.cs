using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShopProject3.API.Helpers;
using ShopProject3.DataAccess;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Services;

namespace ShopProject3.Api
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
            services.AddControllers(setupAction=> {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).AddNewtonsoftJson(
             setupAction =>
             {
                 setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
             }   
            );
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultContext")));
            services.AddRazorPages().AddNewtonsoftJson();
            services.AddMvc().AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });



            services.AddSession();
            services.AddScoped<ShoppingCartHelper>(sp => ShoppingCartHelper.GetCart(sp));
            services.AddHttpContextAccessor();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
