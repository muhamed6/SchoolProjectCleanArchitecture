﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.ApplictionUser
{
    public partial class ApplictionUserProfile : Profile
    {
        public ApplictionUserProfile()
        {
            AddUserMapping();
            GetUserPaginationMapping();
            GetUserByIdMapping();
           EditUserMapping();


        }
    }
}
