using Microsoft.EntityFrameworkCore;
using YourTask.Domain.Models;

namespace YourTask.Infrastructure
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly DbSet<Tarefa> _tarefas;
        private readonly YourTaskContext _context;

        public TarefaRepository(YourTaskContext context)
        {
            _tarefas = context.Tarefas;
            _context = context;
        }

        public Task<List<Tarefa>> ObterTodasTarefas()
        {
            return _tarefas.ToListAsync();
        }

        public ValueTask<Tarefa?> ObterTarefaPorId(int id)
        {
            return _tarefas.FindAsync(id);
        }

        public Task<List<Tarefa>> ObtemTarefasPorPeriodo(DateTime? data = null, StatusTarefa? statusTarefa = null)
        {
            if (data == DateTime.MinValue)
                throw new ArgumentException("Data inicial e final devem ser válidas");

            IQueryable<Tarefa> query = _tarefas.AsQueryable();

            if (data is not null)
                query = query.Where(t => t.DataCriacao.Date == data.Value.Date);

            if (statusTarefa is not null)
                query = query.Where(t => t.StatusTarefa == statusTarefa);

            return query.ToListAsync();
        }

        public async Task CriarTarefa(Tarefa tarefa)
        {
            await _tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
        }

        public Task AtualizarTarefa(Tarefa tarefa)
        {
            _tarefas.Update(tarefa);
            return _context.SaveChangesAsync();
        }

        public async Task AtualizarStatusTarefa(int tarefaId, StatusTarefa status)
        {
            var tarefa = await _tarefas.FindAsync(tarefaId);

            if (tarefa is null)
                throw new KeyNotFoundException("Não foi encontrado a tarefa a ser atualizada");

            tarefa.StatusTarefa = status;

            _tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarTarefa(int tarefaId)
        {
            var tarefa = await _tarefas.FindAsync(tarefaId);

            if (tarefa is null)
                throw new KeyNotFoundException("Não foi encontrado a tarefa a ser excluída");

            _tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
