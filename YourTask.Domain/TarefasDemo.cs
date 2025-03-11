using YourTask.Domain.Models;

namespace YourTask.Domain;

public class TarefasDemo
{
    public static List<Tarefa> CriarTarefasDemo()
    {
        return new List<Tarefa>
        {
            new Tarefa {
                Id = 1,
                Titulo = "Comprar mantimentos",
                Descricao = "Comprar frutas, vegetais e laticínios.",
                DataCriacao = new DateTime(2024, 1, 10),
                DataConclusao = new DateTime(2024, 1, 12),
                StatusTarefa = StatusTarefa.Concluida
            },
            new Tarefa {
                Id = 2,
                Titulo = "Agendar reunião",
                Descricao = "Reunião com a equipe de marketing.",
                DataCriacao = new DateTime(2024, 1, 11),
                DataConclusao = null,
                StatusTarefa = StatusTarefa.EmProgresso
            },
            new Tarefa {
                Id = 3,
                Titulo = "Estudar WPF",
                Descricao = "Revisar conceitos de layout e binding.",
                DataCriacao = new DateTime(2024, 1, 8),
                DataConclusao = null,
                StatusTarefa = StatusTarefa.Pendente
            },
            new Tarefa {
                Id = 4,
                Titulo = "Enviar e-mails",
                Descricao = "Enviar relatórios semanais.",
                DataCriacao = new DateTime(2024, 1, 5),
                DataConclusao = new DateTime(2024, 1, 7),
                StatusTarefa = StatusTarefa.Concluida
            },
            new Tarefa {
                Id = 5,
                Titulo = "Atualizar portfólio",
                Descricao = "Incluir os projetos recentes.",
                DataCriacao = new DateTime(2024, 1, 13),
                DataConclusao = null,
                StatusTarefa = StatusTarefa.EmProgresso
            },
            new Tarefa {
                Id = 6,
                Titulo = "Planejar viagem",
                Descricao = "Pesquisar destinos e reservar passagens.",
                DataCriacao = new DateTime(2024, 1, 6),
                DataConclusao = null,
                StatusTarefa = StatusTarefa.Pendente
            },
            new Tarefa {
                Id = 7,
                Titulo = "Ler livro",
                Descricao = "Terminar leitura do best-seller.",
                DataCriacao = new DateTime(2024, 1, 12),
                DataConclusao = new DateTime(2024, 1, 14),
                StatusTarefa = StatusTarefa.Concluida
            },
            new Tarefa {
                Id = 8,
                Titulo = "Reunião de feedback",
                Descricao = "Sessão de feedback com a equipe de desenvolvimento.",
                DataCriacao = new DateTime(2024, 1, 9),
                DataConclusao = null,
                StatusTarefa = StatusTarefa.EmProgresso
            },
            new Tarefa {
                Id = 9,
                Titulo = "Limpeza geral",
                Descricao = "Limpeza completa no escritório.",
                DataCriacao = new DateTime(2024, 1, 7),
                DataConclusao = new DateTime(2024, 1, 13),
                StatusTarefa = StatusTarefa.Concluida
            },
            new Tarefa {
                Id = 10,
                Titulo = "Desenvolver protótipo",
                Descricao = "Criar um protótipo funcional.",
                DataCriacao = new DateTime(2024, 1, 14),
                DataConclusao = null,
                StatusTarefa = StatusTarefa.Pendente
            }
        };
    }
}
