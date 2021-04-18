using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {

        private readonly VaultsService _vservice;

        private readonly KeepsService _kservice;

        public VaultsController(VaultsService vservice, KeepsService kservice)
        {
            _vservice = vservice;
            _kservice = kservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vault>> GetAllVaults()
        {
            try
            {
                return Ok(_vservice.GetAllVaults());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Vault> GetOneVault(int id)
        {
            try
            {
                return Ok(_vservice.GetOneVault(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> CreateOneVault([FromBody] Vault newVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVault.CreatorId = userInfo.Id;
                newVault.Creator = userInfo;
                return Ok(_vservice.CreateOneVault(newVault));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditOneVault(int id, [FromBody] Vault editVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editVault.CreatorId = userInfo.Id;
                editVault.Id = id;
                editVault.Creator = userInfo;
                _vservice.EditOneVault(editVault);
                return Ok(editVault);
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> DeleteOne(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _vservice.DeleteOneVault(id, userInfo.Id);
                return Ok("Deleted Vault");
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}/keeps")]

        public ActionResult<IEnumerable<VaultKeepViewModel>> GetKeepsByVaultIdAsync(int id)
        {
            try
            {
                return Ok(_kservice.GetKeepsByVaultId(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);                
            }
        }

    }
}