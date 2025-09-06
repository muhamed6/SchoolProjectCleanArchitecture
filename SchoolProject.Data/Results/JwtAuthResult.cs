using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Results
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken  RefreshToken { get; set; }

    }
  public class RefreshToken
    {
        public string UserName { get; set; } = null!;
        public string TokenString { get; set; } = null!;
        public DateTime ExpireAt { get; set; }
    }
}
