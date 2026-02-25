using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SecurityTools;
using SecurityTools.Models;
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
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AccountController(IAccountRepository repository, IOptions<JwtSettings> jwtsetting)
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
                User u = await _repository.GetByEmail(requestDto.Email);
                Dictionary<string,string> infos = new Dictionary<string,string>();
                if(u.Id==2)
                    infos.Add("Level", "9");
                else

                    infos.Add("Level", "10");
                UserInfo ui = new UserInfo()
                    {
                        Id = u.Id.ToString(),
                        Roles = new List<string> { "Admin", "Manager" },
                        MetaData = infos

                    };
                string token = JwtHelper.Generate(_jwtSettings.Value,ui );
                return Ok( new { jwttoken = token });
            }
            else
            {
                return NotFound("NO USERS");
            }
                
            
        }
    }
}
