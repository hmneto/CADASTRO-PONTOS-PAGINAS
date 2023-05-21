using AutoMapper;
using AutoMapperUserProfile;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable

namespace UnitTestsFixed
{
    public static class AutoMapperFix
    {
        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new UserProfile());
                    });

                    IMapper mapper = mappingConfig.CreateMapper();
                    _mapper = mapper;
                }

                return _mapper;
            }
        }
    }
}