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
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _kservice;

        public KeepsController(KeepsService kservice)
        {
            _kservice = kservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Keep>> GetAllKeeps()
        {
            try
            {
                return Ok(_kservice.GetAllKeeps());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Keep> GetOneKeep(int id)
        {
            try
            {
                return Ok(_kservice.GetOneKeep(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> CreateOneKeep([FromBody] Keep newKeep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newKeep.CreatorId = userInfo.Id;
                newKeep.Creator = userInfo;
                return Ok(_kservice.CreateOneKeep(newKeep));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> EditOneKeep(int id, [FromBody] Keep editKeep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editKeep.CreatorId = userInfo.Id;
                editKeep.Id = id;
                editKeep.Creator = userInfo;
                _kservice.EditOneKeep(editKeep);
                return Ok(editKeep);
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> DeleteOneKeep(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
               _kservice.DeleteOne(id, userInfo.Id);
               return Ok("Deleted");
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}