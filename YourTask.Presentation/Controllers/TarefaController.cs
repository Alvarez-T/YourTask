using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YourTask.Domain.Models;
using YourTask.Domain.Validations;
using YourTask.Infrastructure;

namespace YourTask.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaRepository _repository;

    public TarefaController(ITarefaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tarefa>>> GetAll()
    {
        try
        {
            var tarefas = await _repository.ObterTodasTarefas();
            return Ok(tarefas);
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tarefa>> GetById(int id)
    {
        var tarefa = await _repository.ObterTarefaPorId(id);

        if (tarefa is null)
            return NotFound(tarefa);

        return Ok(tarefa);
    }

    [HttpGet("periodo")]
    public async Task<ActionResult<List<Tarefa>>> GetPorPeriodo(
        [FromQuery] StatusTarefa? status = null,
        [FromQuery] DateTime? data = null
        )
    {
        try
        {
            DateTime? dataSelecionada = data == DateTime.MinValue ? null : data;

            var tarefas = await _repository.ObtemTarefasPorPeriodo(dataSelecionada, status);
            return Ok(tarefas);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Tarefa tarefa)
    {
        try
        {
            await _repository.CriarTarefa(tarefa);
            return CreatedAtAction(nameof(GetAll), new { id = tarefa.Id }, tarefa);
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Erro ao criar tarefa: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Tarefa tarefa)
    {
        try
        {
            await _repository.AtualizarTarefa(tarefa);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar tarefa: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repository.DeletarTarefa(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Erro ao excluir tarefa: {ex.Message}");
        }
    }

    [HttpPost("{id}/{status}")]
    public async Task<IActionResult> AlterarStatus(int id, StatusTarefa status)
    {
        try
        {
            await _repository.AtualizarStatusTarefa(id, status);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Erro ao excluir tarefa: {ex.Message}");
        }
    }
}