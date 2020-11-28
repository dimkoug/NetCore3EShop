using AutoMapper;
using ShopProject.Data.Entities;
using ShopProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.API.Profiles
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Appointment, AppointmentEntity>().ReverseMap();
            CreateMap<Banner, BannerEntity>().ReverseMap();
            CreateMap<BannerStatistic, BannerStatisticEntity>().ReverseMap();
            CreateMap<BlogCategory, BlogCategoryEntity>().ReverseMap();
            CreateMap<BlogPostCategory, BlogPostCategoryEntity>().ReverseMap();
            CreateMap<BlogPost, BlogPostEntity>().ReverseMap();
            CreateMap<BlogPostMedia, BlogPostMediaEntity>().ReverseMap();
            CreateMap<BlogPostTag, BlogPostTagEntity>().ReverseMap();
            CreateMap<Brand, BrandEntity>().ReverseMap();
            CreateMap<Event, EventEntity>().ReverseMap();
            CreateMap<EventLocation, EventLocationEntity>().ReverseMap();
            CreateMap<EventMedia, EventMediaEntity>().ReverseMap();
            CreateMap<EventTag, EventTagEntity>().ReverseMap();
            CreateMap<Media, MediaEntity>().ReverseMap();
            CreateMap<Offer, OfferEntity>().ReverseMap();
            CreateMap<OfferDetail, OfferDetailEntity>().ReverseMap();
            CreateMap<Order, OrderEntity>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailEntity>().ReverseMap();
            CreateMap<PressRelease, PressReleaseEntity>().ReverseMap();
            CreateMap<FeatureAttribute, FeatureAttributeEntity>().ReverseMap();
            CreateMap<Product, ProductEntity>().ReverseMap();
            CreateMap<ProductAttribute, ProductAttributeEntity>().ReverseMap();
            CreateMap<Feature, FeatureEntity>().ReverseMap();
            CreateMap<ProductMedia, ProductMediaEntity>().ReverseMap();
            CreateMap<ProductShopCategory, ProductShopCategoryEntity>().ReverseMap();
            CreateMap<ProductTag, ProductTagEntity>().ReverseMap();
            CreateMap<ShopCategory, ShopCategoryEntity>().ReverseMap();
            CreateMap<ShopCategoryFeature, ShopCategoryFeatureEntity>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemEntity>().ReverseMap();
            CreateMap<Tag, TagEntity>().ReverseMap();
        }
    }
}
