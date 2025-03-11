using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using Refit;
using System.Globalization;
using YourTask.Application.Services;
using YourTask.Domain.Models;
using YourTask.Domain.Validations;

namespace YourTask.Application.ViewModels;

public partial class TarefaViewModel : ObservableObject, IDialogResultable<Tarefa?>
{
    private readonly ITarefaApi _tarefaApi;
    private readonly TarefaValidator _validador = new();

    public bool IndicaAlteracao => Id is not null;

    [ObservableProperty] private int? _id;

    [ObservableProperty] private string _titulo;

    [ObservableProperty] private StatusTarefa _status;

    [ObservableProperty] private string _dataCriacao = DateTime.Now.ToString("dd/MM/yyyy");

    [ObservableProperty] private DateTime? _dataConclusao;

    [ObservableProperty] private string _descricao;

    [ObservableProperty] private bool _busyIndicator;

    public Tarefa? Result { get; set; }
    public Action CloseAction { get; set; }

    public TarefaViewModel(ITarefaApi tarefaApi)
    {
        this._tarefaApi = tarefaApi;
    }

    public void CarregarAlteracao(Tarefa tarefa)
    {
        Id = tarefa.Id;
        Titulo = tarefa.Titulo;
        Descricao = tarefa.Descricao!;
        Status = tarefa.StatusTarefa;
        DataCriacao = tarefa.DataCriacao.ToString("dd/MM/yyyy");
        DataConclusao = tarefa.DataConclusao;
    }

    [RelayCommand]
    async Task Salvar()
    {
        BusyIndicator = true;

        try
        {
            var tarefa = CriarTarefa();

            if (!Validar(tarefa))
                return;

            if (IndicaAlteracao)
            {
                await AlterarTarefa(tarefa);
                return;
            }

            var response = await _tarefaApi.Create(tarefa);

            if (!response.IsSuccessful)
            {
                Result = null;
                return;
            }

            Result = response.Content;
            CloseAction();
        }
        finally
        {
            BusyIndicator = false;
        }
    }

    async Task AlterarTarefa(Tarefa tarefa)
    {
        var response = await _tarefaApi.Update(tarefa);

        if (!response.IsSuccessful)
        {
            Result = null;
            return;
        }

        Result = tarefa;
        CloseAction();

        return;
    }

    [RelayCommand]
    void FecharDialog()
    {
        CloseAction();
    }

    bool Validar(Tarefa tarefa)
    {
        var resultadoValidacao = _validador.Validate(tarefa);

        if (!resultadoValidacao.IsValid)
        {
            MessageBox.Warning(string.Join("\n", resultadoValidacao.Errors), "Salvar tarefa");
            return false;
        }

        return true;
    }

    Tarefa CriarTarefa()
    {
        return new Tarefa()
        {
            Id = this.Id ?? 0,
            Titulo = this.Titulo,
            Descricao = this.Descricao,
            StatusTarefa = this.Status,
            DataCriacao = DateTime.ParseExact(this.DataCriacao, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            DataConclusao = this.DataConclusao
        };
    }
}
