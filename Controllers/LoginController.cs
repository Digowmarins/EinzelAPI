using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Einzel.Data;
using Einzel.Models;
using Einzel.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Einzel.Controllers
{
    [ApiController]
    [Route("authentication")]
    public class LoginController : ControllerBase
    {
        private UsuarioContext _context {  get; set; }

        public IMapper _mapper { get; set; }

        public TokenService _tokenservice {  get; set; }

        public EmailService _emailService {  get; set; }

        private readonly UserManager<Usuario> _userManager;

        public LoginController(UsuarioContext context, IMapper mapper, TokenService tokenService, UserManager<Usuario> userManager, EmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _tokenservice = tokenService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
                return NotFound(new { message = "Email ou senha inválidos" });
            }

            if (!await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return NotFound(new { message = "Email ou senha inválidos" });
            }

            var token = _tokenservice.GenerateToken(user);

            return Ok(new { Token = token });
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> EsqueciSenha(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new { Message = "E-mail não encontrado." });
            }

            var token = _tokenservice.GerarTokenRedefinicaoSenha(user);

            string resetLink = $"http://localhost:4200/reset-senha?token={token}";

            string assunto = "Redefinição de Senha";
            string corpoEmail = $"Olá,\n\nVocê solicitou a redefinição de senha. Clique no link abaixo para redefinir sua senha:\n\n{resetLink}";

            _emailService.SendEmail(email, assunto, corpoEmail, "");

            return Ok(new { Message = "Email de redefinição de senha enviado com sucesso." });
        }


        [HttpPost("VerificarEmail/{email}")]
        public async Task<IActionResult> VerificarEmail(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario != null)
            {
                return Ok(new { message = "Email encontrado!" });
            }

            return BadRequest(new { message = "Não achamos nenhuma conta cadastrada com esse e-mail." });
        }
    }
}
