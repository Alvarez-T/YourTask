using YourTask.Domain.Models;

namespace YourTask.Infrastructure
{
    public interface ITarefaRepository
    {
        Task AtualizarStatusTarefa(int tarefaId, StatusTarefa status);
        Task AtualizarTarefa(Tarefa tarefa);
        Task CriarTarefa(Tarefa tarefa);
        Task DeletarTarefa(int tarefaId);
        Task<List<Tarefa>> ObtemTarefasPorPeriodo(DateTime? data = null, StatusTarefa? statusTarefa = null);
        ValueTask<Tarefa?> ObterTarefaPorId(int id);
        Task<List<Tarefa>> ObterTodasTarefas();
    }
}