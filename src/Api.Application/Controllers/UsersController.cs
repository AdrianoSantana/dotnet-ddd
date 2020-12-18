using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 -- Solicitação inválida

            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e) // ArgumentException trata erros de controller
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        // o parâmetro Name serve para chamar internamente dentro do controller essa rota

        public async Task<ActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 -- Solicitação inválida

            try
            {
                return Ok(await _service.Get(id));
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetById([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 -- Solicitação inválida

            try
            {
                var result = await _service.Post(user);
                if (result != null)
                {
                    var uri = new Uri(Url.Link("GetWithId", new { id = result.Id }));
                    // A uri serve para enviar no cabeçalho da resposta http um link para encontrar o objeto criado
                    // Por isso nomeamos a rota getById anteriormente
                    return Created(uri, result);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 -- Solicitação inválida

            try
            {
                var result = await _service.Put(user);
                if (result == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(result);
                }

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 -- Solicitação inválida

            try
            {
                return Ok(await _service.Delete(id));

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }

}
