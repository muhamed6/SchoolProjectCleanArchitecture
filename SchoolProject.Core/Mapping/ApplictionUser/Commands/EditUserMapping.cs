using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
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
        public void EditUserMapping()
        {
            CreateMap<EditUserCommand, User>();
                             
        }
    }
}
