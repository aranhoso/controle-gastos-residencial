using CGR.Application.DTOs;
using CGR.Application.Interfaces;
using CGR.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CGR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Controller responsável por expor os endpoints HTTP de gerenciamento de transações.
/// </summary>
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="TransacoesController"/>.
    /// </summary>
    /// <param name="transacaoService">Serviço de aplicação responsável pelas regras de caso de uso de transações.</param>
    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    /// <summary>
    /// Cria uma nova transação.
    /// </summary>
    /// <param name="dto">Dados necessários para criação da transação.</param>
    /// <returns>
    /// <see cref="CreatedAtActionResult"/> com a transação criada quando a operação for bem-sucedida,
    /// <see cref="NotFoundObjectResult"/> quando pessoa ou categoria não existirem,
    /// ou <see cref="BadRequestObjectResult"/> quando os dados forem inválidos.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] TransacaoRequestDTO dto)
    {
        try
        {
            var transacaoCriada = await _transacaoService.CriarAsync(dto);

            return CreatedAtRoute("GetTransacaoById", new { id = transacaoCriada.Id }, transacaoCriada);
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
    /// Obtém todas as transações cadastradas.
    /// </summary>
    /// <returns>
    /// <see cref="OkObjectResult"/> com a lista de transações,
    /// ou <see cref="ObjectResult"/> com status 500 em caso de erro inesperado.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> ObterTodasAsync()
    {
        try
        {
            var transacoes = await _transacaoService.ObterTodasAsync();
            return Ok(transacoes);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Mensagem = "Ocorreu um erro interno ao listar transações." });
        }
    }

    /// <summary>
    /// Obtém uma transação específica pelo identificador.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>
    /// <see cref="OkObjectResult"/> quando encontrada,
    /// <see cref="NotFoundObjectResult"/> quando não existir,
    /// ou <see cref="ObjectResult"/> com status 500 em caso de erro inesperado.
    /// </returns>
    [HttpGet("{id:guid}", Name = "GetTransacaoById")]
    public async Task<IActionResult> ObterPorIdAsync(Guid id)
    {
        try
        {
            var transacao = await _transacaoService.ObterPorIdAsync(id);

            if (transacao == null)
                return NotFound(new { Mensagem = $"Transação com ID '{id}' não foi encontrado(a)." });

            return Ok(transacao);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Mensagem = "Ocorreu um erro interno ao buscar a transação." });
        }
    }

    /// <summary>
    /// Atualiza os dados de uma transação existente.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <param name="dto">Dados atualizados da transação.</param>
    /// <returns>
    /// <see cref="OkObjectResult"/> com a transação atualizada,
    /// <see cref="NotFoundObjectResult"/> quando a transação, pessoa ou categoria não existirem,
    /// ou <see cref="BadRequestObjectResult"/> quando os dados forem inválidos.
    /// </returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] TransacaoRequestDTO dto)
    {
        try
        {
            var transacaoAtualizada = await _transacaoService.AtualizarAsync(id, dto);
            return Ok(transacaoAtualizada);
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
    /// Remove uma transação existente.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>
    /// <see cref="NoContentResult"/> quando removida com sucesso,
    /// ou <see cref="NotFoundObjectResult"/> quando a transação não existir.
    /// </returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletarAsync(Guid id)
    {
        try
        {
            await _transacaoService.DeletarAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Mensagem = ex.Message });
        }
    }
}
