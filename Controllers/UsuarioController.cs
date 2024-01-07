using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Einzel.Data;
using Einzel.Data.Dtos;
using Einzel.Models;
using Einzel.Services;

namespace Einzel.Controllers
{
    [ApiController]
    [Route("user")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;
        private readonly UsuarioContext _context;
        private readonly EmailService _emailService;

        public UsuarioController(UserManager<Usuario> userManager, IMapper mapper, UsuarioContext context, EmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
        }

        private int GerarNumeroAleatorio()
        {
            // Lógica para gerar um número aleatório
            Random random = new Random();
            return random.Next(10000, 99999);
        }

        private async Task<string> TornarUserNameUnico(string userName)
        {
            // Lógica para tornar o UserName único
            var usuarioExistente = await _userManager.FindByNameAsync(userName);

            int contador = 1;
            while (usuarioExistente != null)
            {
                userName = $"{userName}{contador}";
                usuarioExistente = await _userManager.FindByNameAsync(userName);
                contador++;
            }

            return userName;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CreateUsuarioDto usuarioDto)
        {
            if (ModelState.IsValid)
            {
                string userName = $"{usuarioDto.NomeCompleto.Trim()}{GerarNumeroAleatorio()}";
                userName = await TornarUserNameUnico(userName);

                var usuario = new Usuario
                {
                    NomeCompleto = usuarioDto.NomeCompleto,
                    UserName = userName,
                    Email = usuarioDto.Email,
                    DataCriacao = DateTime.UtcNow,
                    Role = "Cliente"
                };

                var resultadoRegistro = await _userManager.CreateAsync(usuario, usuarioDto.Password);

                if (resultadoRegistro.Succeeded)
                {
                    return Ok("Usuário criado com sucesso");
                }
                else
                {
                    return BadRequest(new { Erros = resultadoRegistro.Errors });
                }
            }

            return BadRequest(ModelState);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObterUsuario(string id)
        {
            var usuario = await _userManager.Users
                .Include(u => u.Enderecos)
                .Include(u => u.Compras)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }

            var usuarioDTO = new
            {
                usuario.Id,
                usuario.UserName,
                usuario.Email,
                usuario.DataCriacao,
                Enderecos = usuario.Enderecos.Select(e => new
                {
                    e.Estado,
                    e.Cidade,
                }),
                Compras = usuario.Compras.Select(c => new
                {
                    c.DataCompra,
                    c.Total,          
                    c.StatusCompra
                })
            };
            return Ok(usuarioDTO);
        }


        [HttpGet]
        public async Task<IActionResult> ObterTodosUsuarios()
        {

            var usuarios = await _userManager.Users.ToListAsync();

            var usuariosDTO = usuarios.Select(u => new
            {
                u.UserName,
                u.Email,
                u.DataCriacao,
            });

            return Ok(usuariosDTO);
        }

        [HttpPost("endereco")]
        public IActionResult AdicionarEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            try
            {
                var endereco = _mapper.Map<Endereco>(enderecoDto);
                if (endereco == null)
                {
                    return BadRequest();
                }
                _context.Enderecos.Add(endereco);
                _context.SaveChanges();
                return Ok(endereco);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("endereco")]
        public ActionResult<IEnumerable<ReadEnderecoDto>> RecuperarEnderecos(string id)
        {
            try
            {
                var enderecos = _context.Enderecos.FirstOrDefault(u => u.UsuarioId == id);

                return Ok(enderecos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("compra")]
        public IActionResult AdicionarCompra([FromBody] CreateCompraDto compraDto)
        {
            try
            {
                var compra = _mapper.Map<Compra>(compraDto);
                _context.Compras.Add(compra);
                _context.SaveChanges();
                return Ok(compra);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Erro ao adicionar compra: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Exceção Interna: {ex.InnerException.Message}");
                }
             
                return BadRequest("Ocorreu um erro ao processar a solicitação. Consulte os logs para obter detalhes.");
            }
        }

        [HttpGet("compras")]
        public IActionResult ObterTodasCompras()
        {
            try
            {
                var compras = _context.Compras
                     .Include(c => c.Usuario)
                     .Include(c => c.EnderecoEntrega)
                     .ToList();

                var comprasDto = _mapper.Map<List<ReadCompraDto>>(compras);

                return Ok(comprasDto);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Erro ao obter todas as compras: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Exceção Interna: {ex.InnerException.Message}");
                }
                
                return BadRequest("Ocorreu um erro ao processar a solicitação. Consulte os logs para obter detalhes.");
            }
        }

        [HttpGet("compras/{id}")]
        public ActionResult<IEnumerable<ReadCompraDto>> ObterCompraPorID(string id)
        {
            try
            {
                var compras = _context.Compras.Where(c => c.UsuarioId == id).ToList();
                if (compras == null)
                {
                    return NotFound();
                }
                var comprasDto = _mapper.Map<IEnumerable<ReadCompraDto>>(compras);
                return Ok(comprasDto);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("adicionar-codigo-rastreio")]
        public IActionResult AdicionarCodigoRastreio([FromBody] AdicionarCodigoRastreioDto dto)
        {
            try
            {
                var compra = _context.Compras.FirstOrDefault(c => c.Id == dto.CompraId);

                if (compra == null)
                {
                    return NotFound("Compra não encontrada");
                }

                var usuario = _context.Users.FirstOrDefault(u => u.Id == compra.UsuarioId);
                if (usuario != null)
                {
                    var email = usuario.Email;
                    if (email != null)
                    {
                        string codigoRastreio = "QP754458195BR";

                        string subject = "Seu produto foi postado nos Correios!";
                        string body = $"Prezado(a) {usuario.NomeCompleto},\n\n"
                                    + "Seu produto foi postado nos Correios com sucesso. Aqui está o código de rastreio:\n\n"
                                    + $"{codigoRastreio}\n\n"
                                    + "Você pode usar este código para rastrear a entrega do seu produto.\n\n"
                                    + "Obrigado por escolher nossos serviços!\n\n"
                                    + "Atenciosamente,\n"
                                    + "Einzel";

                        _emailService.SendEmail(email, subject, body, usuario.NomeCompleto);
                    }
                }   

                compra.CodigoRastreio = dto.CodigoRastreio;
                compra.StatusCompra = "Enviado";
                _context.SaveChanges();

                return Ok(new { Message = "Código de rastreio adicionado com sucesso." });
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Erro ao adicionar código de rastreio: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Exceção Interna: {ex.InnerException.Message}");
                }

                return BadRequest("Ocorreu um erro ao processar a solicitação. Consulte os logs para obter detalhes.");
            }
        }
    }
}

