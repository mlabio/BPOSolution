using AutoMapper;
using BPOSolution.Models;
using BPOSolution.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BPOSolution.ConfigServices
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<BPOClient, BPOClientViewModel>().ReverseMap();

        }

        private void AfterMap(Func<object, object, object> p)
        {
            throw new NotImplementedException();
        }
    }

    public static class MapperConfigService
    {
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();

            return services;
        }
    }
}
