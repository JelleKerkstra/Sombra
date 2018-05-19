﻿using AutoMapper;

namespace Sombra.CharityActionService.UnitTests
{
    public static class Helper
    {
        public static IMapper GetMapper()
        {
            if (MapperInstance != null) return MapperInstance;

            Mapper.Initialize(cfg => cfg.AddProfile(new MappingProfile()));
            var config = Mapper.Configuration;
            config.AssertConfigurationIsValid();
            MapperInstance = new Mapper(config);

            return MapperInstance;
        }

        private static IMapper MapperInstance { get; set; }
    }
}