using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SecurityTools;
using SecurityTools.Models;
using TestSecu.Domain;
using TestSecu.Domain.Repositories;
using TestSecu.Domain.Services;
using TestSecu.dto; 
namespace TestSecu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AccountController(IAccountRepository repository,IJWtService jWtService, IOptions<JwtSettings> jwtsetting)
        {
            _repository = repository;
            _jwtSettings = jwtsetting;
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
                //recupérer le user en "DB"
                string token = 
                return Ok( new { jwttoken = token });
            }
            else
            {
                return NotFound("NO USERS");
            }
                
            
        }
    }
}
