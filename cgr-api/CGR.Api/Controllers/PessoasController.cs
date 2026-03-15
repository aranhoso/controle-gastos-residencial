using CGR.Application.DTOs;
using CGR.Application.Interfaces;
using CGR.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CGR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Controller responsável por expor os endpoints HTTP de gerenciamento de pessoas.
/// </summary>
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="PessoasController"/>.
    /// </summary>
    /// <param name="pessoaService">Serviço de aplicação responsável pelas regras de caso de uso de pessoas.</param>
    public PessoasController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    /// <summary>
    /// Cria uma nova pessoa.
    /// </summary>
    /// <param name="dto">Dados necessários para criação da pessoa.</param>
    /// <returns>
    /// <see cref="CreatedAtActionResult"/> com a pessoa criada quando a operação for bem-sucedida,
    /// ou <see cref="BadRequestObjectResult"/> quando os dados forem inválidos.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] PessoaRequestDTO dto)
    {
        try
        {
            var pessoaCriada = await _pessoaService.CriarAsync(dto);

            return CreatedAtRoute("GetPessoaById", new { id = pessoaCriada.Id }, pessoaCriada);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Mensagem = ex.Message });
        }
    }

    /// <summary>
    /// Obtém todas as pessoas cadastradas.
    /// </summary>
    /// <returns>
    /// <see cref="OkObjectResult"/> com a lista de pessoas,
    /// ou <see cref="ObjectResult"/> com status 500 em caso de erro inesperado.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> ObterTodasAsync()
    {
        try
        {
            var pessoas = await _pessoaService.ObterTodasAsync();
            return Ok(pessoas);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Mensagem = "Ocorreu um erro interno ao listar pessoas." });
        }
    }

    /// <summary>
    /// Obtém uma pessoa específica pelo identificador.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>
    /// <see cref="OkObjectResult"/> quando encontrada,
    /// <see cref="NotFoundObjectResult"/> quando não existir,
    /// ou <see cref="ObjectResult"/> com status 500 em caso de erro inesperado.
    /// </returns>
    [HttpGet("{id:guid}", Name = "GetPessoaById")]
    public async Task<IActionResult> ObterPorIdAsync(Guid id)
    {
        try
        {
            var pessoa = await _pessoaService.ObterPorIdAsync(id);

            if (pessoa == null)
                return NotFound(new { Mensagem = $"Pessoa com ID '{id}' não foi encontrado(a)." });

            return Ok(pessoa);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Mensagem = "Ocorreu um erro interno ao buscar a pessoa." });
        }
    }

    /// <summary>
    /// Atualiza os dados de uma pessoa existente.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <param name="dto">Dados atualizados da pessoa.</param>
    /// <returns>
    /// <see cref="OkObjectResult"/> com a pessoa atualizada,
    /// <see cref="NotFoundObjectResult"/> quando a pessoa não existir,
    /// ou <see cref="BadRequestObjectResult"/> quando os dados forem inválidos.
    /// </returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] PessoaRequestDTO dto)
    {
        try
        {
            var pessoaAtualizada = await _pessoaService.AtualizarAsync(id, dto);
            return Ok(pessoaAtualizada);
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
    /// Remove uma pessoa existente.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> quando removida com sucesso,
    /// ou <see cref="NotFoundObjectResult"/> quando a pessoa não existir.
    /// </returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletarAsync(Guid id)
    {
        try
        {
            await _pessoaService.DeletarAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Mensagem = ex.Message });
        }
    }
}
