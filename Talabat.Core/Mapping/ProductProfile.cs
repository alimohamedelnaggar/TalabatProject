using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.Product;
using Talabat.Core.Entities;

namespace Talabat.Core.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(d => d.PictureUrl, opt => opt.MapFrom(s => $"{configuration["BASEURL"]}{s.PictureUrl}"))
                .ReverseMap();
        }
    }
    
}
