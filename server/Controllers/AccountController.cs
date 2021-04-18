using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using server.Models;
using server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _ps;

        private readonly KeepsService _kservice;

        private readonly VaultsService _vservice;

        public AccountController(ProfilesService ps, KeepsService kservice, VaultsService vservice)
        {
            _ps = ps;
            _kservice = kservice;
            _vservice = vservice;
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByAccountIdAsync()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vservice.GetVaultsByAccountId(userInfo.Id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}