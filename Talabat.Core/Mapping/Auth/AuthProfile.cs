using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.Auth;
using Talabat.Core.Identity;

namespace Talabat.Core.Mapping.Auth
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
