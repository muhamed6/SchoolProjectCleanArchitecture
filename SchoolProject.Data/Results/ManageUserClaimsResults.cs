using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Results
{
    public class ManageUserClaimsResults
    {
        public int UserId { get; set; }
        public List<UserClaims> UserClaims { get; set; }
    }

    public class UserClaims
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}