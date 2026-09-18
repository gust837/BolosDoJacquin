using BolosDoJacquin.DTOs;
using BolosDoJacquin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BolosDoJacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _usuarioRepository;

        public LoginController(IUsuario usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            
                var usuario = await _usuarioRepository.BuscarPorEmailESenha(dto.Email, dto.Senha);

                if (usuario == null)
                {
                    return Unauthorized("Email ou senha inválidos.");
                }

                if (usuario.Situacao == "Inativo")
                {
                    return BadRequest("Esta conta está desativada.");
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Perfil),
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString())
                };

                var chaveConfig = HttpContext.RequestServices.GetService<IConfiguration>()?.GetValue<string>("JwtSecret") ?? "fallback-secreta-api-bolos-jacquin-senai-12345678";
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveConfig));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "BolosDoJacquin",
                    audience: "BolosDoJacquin",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            
        }
    }
}

