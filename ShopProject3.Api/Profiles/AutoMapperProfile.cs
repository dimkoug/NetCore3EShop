using AutoMapper;
using ShopProject3.DataAccess.Entities;
using ShopProject3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject3.API.Profiles
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Categories, CategoriesEntity>().ReverseMap();
            CreateMap<Brands, BrandsEntity>().ReverseMap();
            CreateMap<Tags, TagsEntity>().ReverseMap();
            CreateMap<Media, MediaEntity>().ReverseMap();
            CreateMap<Features, FeaturesEntity>().ReverseMap();
            CreateMap<FeatureAttributes, FeatureAttributesEntity>().ReverseMap();
            CreateMap<CategoryFeatures, CategoryFeaturesEntity>().ReverseMap();
            CreateMap<Products, ProductsEntity>().ReverseMap();
            CreateMap<ShoppingCartItems, ShoppingCartItemsEntity>().ReverseMap();
            CreateMap<ProductCategories, ProductCategoriesEntity>().ReverseMap();
            CreateMap<ProductTags, ProductTagsEntity>().ReverseMap();
            CreateMap<ProductMedia, ProductMediaEntity>().ReverseMap();
            CreateMap<ProductFeatureAttributes, ProductFeatureAttributesEntity>().ReverseMap();
            CreateMap<Orders, OrdersEntity>().ReverseMap();
            CreateMap<OrdersDetail, OrdersDetailEntity>().ReverseMap();
        }
    }
}
