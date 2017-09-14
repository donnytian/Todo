using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Application;
using Todo.Application.Dto;
using Todo.Common.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todo.Web.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : TodoControllerBase
    {
        private readonly ITodoAppService _appService;

        public TodoController(ITodoAppService appService, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _appService = appService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await _appService.FindAllAsync();

                return Ok(list);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while getting items");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TodoItemDto item)
        {
            try
            {
                item = await _appService.UpdateOrInsertItemAsync(item);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating or updating item");
            }

            return Ok(item);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _appService.DeleteItemAsync(id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while deleting item");
            }

            return Ok();
        }
    }
}
