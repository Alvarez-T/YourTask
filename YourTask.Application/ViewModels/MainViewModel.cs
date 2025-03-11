using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using YourTask.Application.Services;
using YourTask.Domain.Models;

namespace YourTask.Application.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ITarefaApi _tarefaApi;

    [ObservableProperty] private DateTime? _filtroDataCriacao;

    [ObservableProperty] private StatusTarefa? _filtroStatus;

    [ObservableProperty] private ObservableCollection<Tarefa> _tarefas;

    [ObservableProperty] private bool _busyIndicator;

    public MainViewModel(ITarefaApi tarefaApi)
    {
        this._tarefaApi = tarefaApi;
    }

    public async Task CarregarTarefas()
    {
        BusyIndicator = true;

        try
        {
            var response = await _tarefaApi.GetAll();

            if (!response.IsSuccessful || response.Content is null)
                return;

            Tarefas = new ObservableCollection<Tarefa>(response.Content);
        }
        finally
        {
            BusyIndicator = false;
        }
    }

    [RelayCommand]
    async Task Filtrar()
    {
        if (FiltroDataCriacao is null && FiltroStatus is null)
        {
            MessageBox.Warning("Nenhum filtro aplicado", "Filtrar tarefas");
            return;
        }

        BusyIndicator = true;

        try
        {
            int? status = FiltroStatus is not null ? (int)FiltroStatus : null;

            var response = await _tarefaApi.GetPorPeriodo(status, FiltroDataCriacao);

            if (!response.IsSuccessful || response.Content is null)
                return;

            Tarefas = new ObservableCollection<Tarefa>(response.Content);
        } 
        finally
        {
            BusyIndicator = false;
        }
    }

    [RelayCommand]
    async Task LimparFiltros()
    {
        FiltroDataCriacao = null;
        FiltroStatus = null;

        await CarregarTarefas();
    }

    [RelayCommand]
    async Task AdicionarTarefa()
    {
        var tarefa = await Dialog.Show<TarefaView>()
            .Initialize<TarefaViewModel>((_) => { }) //necessário para dar gatilho no CloseAction
            .GetResultAsync<Tarefa?>();

        if (tarefa is null)
            return;

        Tarefas.Add(tarefa);
    }

    [RelayCommand]
    async Task EditarSelecionada(Tarefa tarefa)
    {
        int index = Tarefas.IndexOf(tarefa);

        tarefa = await Dialog.Show<TarefaView>()
              .Initialize<TarefaViewModel>(vm => vm.CarregarAlteracao(tarefa))
              .GetResultAsync<Tarefa>();

        AtualizarTarefaSelecionada(index, tarefa);
    }

    [RelayCommand]
    async Task DeletarSelecionada(Tarefa tarefa)
    {
        var result = MessageBox.Ask($"Deseja realmente excluir a tarefa \"{tarefa.Titulo}\"?", "Excluir tarefa?");
        if (result == System.Windows.MessageBoxResult.No)
            return;
 
        var response = await _tarefaApi.Delete(tarefa.Id);

        if (!response.IsSuccessful)
            return;

        Tarefas.Remove(tarefa);
    }

    [RelayCommand]
    Task ResetarTarefa(Tarefa tarefa) => AlterarStatusTarefa(tarefa, StatusTarefa.Pendente);

    [RelayCommand]
    Task IniciarTarefa(Tarefa tarefa) => AlterarStatusTarefa(tarefa, StatusTarefa.EmProgresso);

    [RelayCommand]
    Task ConcluirTarefa(Tarefa tarefa) => AlterarStatusTarefa(tarefa, StatusTarefa.Concluida);

    async Task AlterarStatusTarefa(Tarefa tarefa, StatusTarefa status)
    {
        var response = await _tarefaApi.AlterarStatus(tarefa.Id, status);

        if (!response.IsSuccessful)
            return;

        tarefa.StatusTarefa = status;
        int index = Tarefas.IndexOf(tarefa);
        AtualizarTarefaSelecionada(index, tarefa);
    }

    void AtualizarTarefaSelecionada(int index, Tarefa tarefa)
    {
        Tarefas[index] = new Tarefa
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            DataCriacao = tarefa.DataCriacao,
            DataConclusao = tarefa.DataConclusao,
            StatusTarefa = tarefa.StatusTarefa
        };
    }
}
