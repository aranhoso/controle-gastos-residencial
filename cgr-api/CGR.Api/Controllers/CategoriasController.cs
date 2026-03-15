using CGR.Application.DTOs;
using CGR.Application.Interfaces;
using CGR.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CGR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Controller responsável por expor os endpoints HTTP de gerenciamento de categorias.
/// </summary>
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CategoriasController"/>.
    /// </summary>
    /// <param name="categoriaService">Serviço de aplicação responsável pelas regras de caso de uso de categorias.</param>
    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    /// <summary>
    /// Cria uma nova categoria.
    /// </summary>
    /// <param name="dto">Dados necessários para criação da categoria.</param>
    /// <returns>
    /// <see cref="CreatedAtActionResult"/> com a categoria criada quando a operação for bem-sucedida,
    /// ou <see cref="BadRequestObjectResult"/> quando os dados forem inválidos.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CategoriaRequestDTO dto)
    {
        try
        {
            var categoriaCriada = await _categoriaService.CriarAsync(dto);

            return CreatedAtAction(nameof(ObterPorIdAsync), new { id = categoriaCriada.Id }, categoriaCriada);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Mensagem = ex.Message });
        }
    }

    /// <summary>
    /// Obtém todas as categorias cadastradas.
    /// </summary>
    /// <returns>
    /// <see cref="OkObjectResult"/> com a lista de categorias,
    /// ou <see cref="ObjectResult"/> com status 500 em caso de erro inesperado.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> ObterTodasAsync()
    {
        try
        {
            var categorias = await _categoriaService.ObterTodasAsync();
            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Mensagem = "Ocorreu um erro interno ao listar categorias." });
        }
    }

    /// <summary>
    /// Obtém uma categoria específica pelo identificador.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <returns>
    /// <see cref="OkObjectResult"/> quando encontrada,
    /// <see cref="NotFoundObjectResult"/> quando não existir,
    /// ou <see cref="ObjectResult"/> com status 500 em caso de erro inesperado.
    /// </returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorIdAsync(Guid id)
    {
        try
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);

            if (categoria == null)
                return NotFound(new { Mensagem = $"Categoria com ID '{id}' não foi encontrado(a)." });

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Mensagem = "Ocorreu um erro interno ao buscar a categoria." });
        }
    }

    /// <summary>
    /// Atualiza os dados de uma categoria existente.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <param name="dto">Dados atualizados da categoria.</param>
    /// <returns>
    /// <see cref="OkObjectResult"/> com a categoria atualizada,
    /// <see cref="NotFoundObjectResult"/> quando a categoria não existir,
    /// ou <see cref="BadRequestObjectResult"/> quando os dados forem inválidos.
    /// </returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] CategoriaRequestDTO dto)
    {
        try
        {
            var categoriaAtualizada = await _categoriaService.AtualizarAsync(id, dto);
            return Ok(categoriaAtualizada);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Mensagem = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Mensagem = ex.Message });
        }
    }

    /// <summary>
    /// Remove uma categoria existente.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> quando removida com sucesso,
    /// ou <see cref="NotFoundObjectResult"/> quando a categoria não existir.
    /// </returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletarAsync(Guid id)
    {
        try
        {
            await _categoriaService.DeletarAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Mensagem = ex.Message });
        }
    }
}