using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestSecu.Domain;
using TestSecu.Domain.Repositories;
using TestSecu.dto;
namespace TestSecu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;

        public AccountController(IAccountRepository repository)
        {
            _repository = repository;
        }


        [HttpPost]
        public async Task<IActionResult>Authenticate([FromBody]UserRequestDto requestDto)
        {
            //
            //if (requestDto is  null || requestDto.Email is null || requestDto.Password is null) return BadRequest();
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Vérifier si user existe
            if(await _repository.Authenticate(requestDto.Email, requestDto.Password))
            {
                //Générer mon token pour l'envoyer

                return Ok();
            }
            else
            {
                return NotFound("NO USERS");
            }
                
            
        }
    }
}
