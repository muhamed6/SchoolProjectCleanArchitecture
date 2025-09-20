using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Emails.Commands.Handlers
{
    public class EmailsCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #region Fields

        #endregion

        #region Constructors
        public EmailsCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IEmailService emailService) : base(stringLocalizer)
        {
            _emailService = emailService;
            _localizer = stringLocalizer;
        }

        #endregion

        #region HandleFunctions

       public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
           var response = await _emailService.SendEmail(request.Email, request.Message);

            if (response == "Success")
                return Success<string>("");


            return BadRequest<string>(_localizer[SharedResourcesKeys.SendEmailFailed]);
        }
        #endregion
    }
}
