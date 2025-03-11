using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTask.Domain.Models;

namespace YourTask.Application
{
    public static class DesignData
    {
        public static ObservableCollection<Tarefa> LoadDesignData()
        {
            return new ObservableCollection<Tarefa>
            {
                new Tarefa {
                    Titulo = "Comprar mantimentos",
                    Descricao = "Comprar frutas, vegetais e laticínios.",
                    DataCriacao = DateTime.Now.AddDays(-5),
                    DataConclusao = DateTime.Now.AddDays(-3),
                    StatusTarefa = StatusTarefa.Concluida
                },
                new Tarefa {
                    Titulo = "Agendar reunião",
                    Descricao = "Reunião com a equipe de marketing.",
                    DataCriacao = DateTime.Now.AddDays(-4),
                    DataConclusao = null,
                    StatusTarefa = StatusTarefa.EmProgresso
                },
                new Tarefa {
                    Titulo = "Estudar WPF",
                    Descricao = "Revisar conceitos de layout e binding.",
                    DataCriacao = DateTime.Now.AddDays(-7),
                    DataConclusao = null,
                    StatusTarefa = StatusTarefa.Pendente
                },
                new Tarefa {
                    Titulo = "Enviar e-mails",
                    Descricao = "Enviar relatórios semanais.",
                    DataCriacao = DateTime.Now.AddDays(-10),
                    DataConclusao = DateTime.Now.AddDays(-8),
                    StatusTarefa = StatusTarefa.Concluida
                },
                new Tarefa {
                    Titulo = "Atualizar portfólio",
                    Descricao = "Incluir os projetos recentes.",
                    DataCriacao = DateTime.Now.AddDays(-2),
                    DataConclusao = null,
                    StatusTarefa = StatusTarefa.EmProgresso
                },
                new Tarefa {
                    Titulo = "Planejar viagem",
                    Descricao = "Pesquisar destinos e reservar passagens.",
                    DataCriacao = DateTime.Now.AddDays(-9),
                    DataConclusao = null,
                    StatusTarefa = StatusTarefa.Pendente
                },
                new Tarefa {
                    Titulo = "Ler livro",
                    Descricao = "Terminar leitura do best-seller.",
                    DataCriacao = DateTime.Now.AddDays(-3),
                    DataConclusao = DateTime.Now.AddDays(-1),
                    StatusTarefa = StatusTarefa.Concluida
                },
                new Tarefa {
                    Titulo = "Reunião de feedback",
                    Descricao = "Sessão de feedback com a equipe de desenvolvimento.",
                    DataCriacao = DateTime.Now.AddDays(-6),
                    DataConclusao = null,
                    StatusTarefa = StatusTarefa.EmProgresso
                },
                new Tarefa {
                    Titulo = "Limpeza geral",
                    Descricao = "Limpeza completa no escritório.",
                    DataCriacao = DateTime.Now.AddDays(-8),
                    DataConclusao = DateTime.Now.AddDays(-2),
                    StatusTarefa = StatusTarefa.Concluida
                },
                new Tarefa {
                    Titulo = "Desenvolver protótipo",
                    Descricao = "Criar um protótipo funcional.",
                    DataCriacao = DateTime.Now.AddDays(-1),
                    DataConclusao = null,
                    StatusTarefa = StatusTarefa.Pendente
                }
            };
        }
    }
}
