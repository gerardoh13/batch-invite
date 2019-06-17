using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.Domain;
using Models.Requests;
using Models.Requests.Emails;
using Services;
using Web.Controllers;
using Web.Models.Responses;

namespace Web.Api.Controllers
{
    [Route("api/contributors")]
    [ApiController]
    public class ContributorApiController : BaseApiController
    {

        private IContributorService _contributorService = null;
        private IAuthenticationService<int> _authService;
        private IEmailService _emailService = null;
        private ITokenService _tokenService;

        public ContributorApiController(IContributorService contributorService, IAuthenticationService<int> authService, IEmailService emailService, ITokenService tokenService, ILogger<ContributorApiController> logger) : base(logger)
        {
            _authService = authService;
            _contributorService = contributorService;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        [HttpGet("confirm")]
        public ActionResult<SuccessResponse> Confirm(string token)

        {
            try
            {
                InviteContributor model = _tokenService.ConfirmContributor(token);
                _contributorService.UpdateConfirmStatus(model.Id);
                _contributorService.InsertContributors(model);
                SuccessResponse response = new SuccessResponse();
                return Ok200(response);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return StatusCode(500, new ErrorResponse(ex.Message));
            }
        }

        [HttpPost("invite")]
        public async Task<ActionResult<ItemResponse<int>>> Add(List<InviteContributorRequest> model)
        {

            try
            {
                SuccessResponse response = new SuccessResponse();

                _contributorService.Invite_Contributors(model);

                foreach (var item in model)
                {
                    await _emailService.InviteContributor(item);

                }
                return Ok200(response);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return StatusCode(500, new ErrorResponse(ex.Message));
            }
        }

    }
}