using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {

        #region Fields
        private readonly UserManager<User> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly ApplicationDBContext _dbContext;
        private readonly IUrlHelper _urlHelper;

        #endregion

        #region Constructors

        public ApplicationUserService(UserManager<User> userManager,
                                      IHttpContextAccessor httpContextAccessor,
                                      IEmailService emailService,
                                      ApplicationDBContext dbContext,
                                      IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _dbContext = dbContext;
            _urlHelper = urlHelper;
        }

        #endregion

        #region Handle Functions
        public async Task<string> AddUserAsync(User user, string password)
        {

            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {

            var existUser = await _userManager.FindByEmailAsync(user.Email);

            if (existUser != null)
                return "EmailIsExist";

            var userByUserName = await _userManager.FindByNameAsync(user.UserName);

            if (userByUserName != null)
                return "UserNameIsExist";

         
            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
                //return string.Concat(createResult.Errors);
                return string.Join(", ",createResult.Errors.Select(x => x.Description).ToList());


            await _userManager.AddToRoleAsync(user, "User");

            // Send Confirm Email

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var requestAccessor = _httpContextAccessor.HttpContext.Request;

            var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new {userId = user.Id, code = code});

            // Message Or Body
            var sendEmailResult = await _emailService.SendEmail(user.Email, returnUrl);
           

            await trans.CommitAsync();
            return ("Success");


            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
          return ("Failed");
            }
        }
        #endregion
    }
}
