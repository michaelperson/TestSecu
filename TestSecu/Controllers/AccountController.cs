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
        private readonly IAccountRepository _accountRepository;
        private readonly IJWtService _jwtservice;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AccountController(IAccountRepository repository,IJWtService jWtService, IOptions<JwtSettings> jwtsetting)
        {
            _accountRepository = repository;
            _jwtSettings = jwtsetting;
            _jwtservice = jWtService;
        }


        [HttpPost]
        public async Task<IActionResult>Authenticate([FromBody]UserRequestDto requestDto)
        { 
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Vérifier si user existe
            if(await _accountRepository.Authenticate(requestDto.Email, requestDto.Password))
            {
                //Générer mon token pour l'envoyer
                //recupérer le user en "DB"
                User u = await _accountRepository.GetByEmail(requestDto.Email);
                string token = await _jwtservice.GetToken(u.Id, _jwtSettings.Value);
                return Ok( new { jwttoken = token });
            }
            else
            {
                return NotFound("NO USERS");
            }
                
            
        }
    }
}
