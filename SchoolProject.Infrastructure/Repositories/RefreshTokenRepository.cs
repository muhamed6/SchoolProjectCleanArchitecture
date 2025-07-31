using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {


        #region Fields
        private DbSet<UserRefreshToken> userRefreshToken;
        #endregion

        #region Constructors


        public RefreshTokenRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            userRefreshToken = dbContext.Set<UserRefreshToken>();
        }
        #endregion

        #region Actions

        #endregion
    }
}
