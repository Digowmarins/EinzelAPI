using AutoMapper;
using Einzel.Data;
using Einzel.Data.Dtos;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Einzel.Services;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Einzel.Models;

namespace Einzel.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutoController : ControllerBase
    {
        public IMapper _mapper { get; set; }
        public ProdutoContext _context { get; set; }

        public ProdutoController(IMapper mapper, ProdutoContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarProduto([FromBody] CreateProdutoDto produtoDto)
        {
            try
            {
                Produto produto = _mapper.Map<Produto>(produtoDto);

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                var produtoJson = System.Text.Json.JsonSerializer.Serialize(produto, options);

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                return CreatedAtAction(nameof(RecuperarProdutosPorId), new { Id = produto.Id }, produtoJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<ReadProdutoDto>> RecuperarProdutos()
        {
            try
            {
                var produtos = _context.Produtos
                    .Include(p => p.Variacoes)
                        .ThenInclude(v => v.Medidas)
                    .ToList();

                var produtosDto = _mapper.Map<List<ReadProdutoDto>>(produtos);

                return Ok(produtosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarProdutosPorId(int id)
        {
            try
            {
                Produto produto = _context.Produtos
                    .Include(p => p.Variacoes)
                        .ThenInclude(v => v.Medidas)
                    .FirstOrDefault(u => u.Id == id);

                if (produto != null)
                {
                    ReadProdutoDto produtoDto = _mapper.Map<ReadProdutoDto>(produto);
                    return Ok(produtoDto);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarProduto(int id)
        {
            try
            {
                Produto produto = _context.Produtos.FirstOrDefault(u => u.Id == id);
                if (produto == null)
                {
                    return NotFound();
                }
                var variacoes = _context.VariacoesTamanho
                        .Where(vt => vt.ProdutoId == id)
                        .ToList();


                foreach (var vari in variacoes)
                {
                    var medidas = _context.Medidas
                    .Where(m => m.VariacaoTamanhoId == vari.Id)
                    .ToList();
                    foreach (var medi in medidas)
                    {
                        _context.Remove(medi);
                    }
                    _context.Remove(vari);
                }
                _context.Remove(produto);
                _context.SaveChanges();
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost("imagens/{produtoId}")]
        public async Task<IActionResult> UploadFotos(int produtoId, [FromForm] List<IFormFile> fotos)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.Id == produtoId);

                if (produto == null)
                {
                    return NotFound("Produto não encontrado.");
                }

                if (fotos == null || fotos.Count == 0)
                {
                    return BadRequest("Nenhuma foto foi enviada.");
                }

                var imagens = new List<ImagemProduto>();

                foreach (var foto in fotos)
                {
                    if (foto.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await foto.CopyToAsync(memoryStream);
                            var imagemBytes = memoryStream.ToArray();

                            var imagemProduto = new ImagemProduto
                            {
                                DadosImagem = imagemBytes,
                                Produto = produto
                            };

                            imagens.Add(imagemProduto);
                        }
                    }
                }

                _context.ImagensProdutos.AddRange(imagens);
                _context.SaveChanges();

                return Ok(new { mensagem = "Fotos enviadas com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao fazer o upload das fotos: {ex.Message}");
            }
        }

        [HttpGet("imagens/{productId}")]
        public IActionResult GetImagensByProdutoId(int productId)
        {
            try
            {
                var imagens = _context.ImagensProdutos.Where(i => i.ProdutoId == productId).ToList();

                if (imagens == null || imagens.Count == 0)
                {
                    return NotFound();
                }

                List<string> imagemBytesList = new List<string>();

                foreach (var imagem in imagens)
                {
                    string imagemBytes = Convert.ToBase64String(imagem.DadosImagem);
                    imagemBytesList.Add(imagemBytes);
                }

                return new JsonResult(imagemBytesList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("banner")]
        public IActionResult UploadBanner([FromForm] List<IFormFile> fotoBanner)
        {
            try
            {
                if (fotoBanner != null && fotoBanner.Count > 0)
                {
                    foreach (var file in fotoBanner)
                    {
                        if (file.Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                file.CopyTo(memoryStream);
                                var banner = new Banner
                                {
                                    DadosImagem = memoryStream.ToArray()
                                };

                                _context.Banners.Add(banner);
                            }
                        }
                    }

                    _context.SaveChanges();

                    return Ok(new { mensagem = "Fotos enviadas com sucesso." });
                }
                else
                {
                    return BadRequest("Nenhuma imagem recebida.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("banners")]
        public IActionResult GetAllBanners()
        {
            try
            {
                var banners = _context.Banners.ToList();

                if (banners != null && banners.Count > 0)
                {                 
                    var imagesData = new List<byte[]>();
                   
                    foreach (var banner in banners)
                    {
                        imagesData.Add(banner.DadosImagem);
                    }

                    return Ok(imagesData);
                }
                else
                {
                    return NotFound("Nenhum banner encontrado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
