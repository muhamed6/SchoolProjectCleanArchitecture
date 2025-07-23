﻿using SchoolProject.Core.Features.ApplicationUser.Queries.Results;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Mapping.ApplictionUser
{
    public partial class ApplictionUserProfile
    {
        public void GetUserPaginationMapping()
        {
            CreateMap<User, GetUserPaginationResponse>();
        }
    }
}
