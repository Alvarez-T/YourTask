using Refit;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using YourTask.Domain.Models;

namespace YourTask.Application.Services;

public interface ITarefaApi
{
    // GET api/tarefa
    [Get("/api/tarefa")]
    Task<IApiResponse<List<Tarefa>>> GetAll();

    // GET api/tarefa/{id}
    [Get("/api/tarefa/{id}")]
    Task<IApiResponse<Tarefa>> GetById(int id);

    [Get("/api/tarefa/periodo")]
    Task<IApiResponse<List<Tarefa>>> GetPorPeriodo(
        [Query] int? status = null,
        [Query] DateTime? data = null
        );

    // POST api/tarefa
    [Post("/api/tarefa")]
    Task<IApiResponse<Tarefa>> Create([Body] Tarefa tarefa);

    // PUT api/tarefa
    [Put("/api/tarefa")]
    Task<IApiResponse> Update([Body] Tarefa tarefa);

    // DELETE api/tarefa/{id}
    [Delete("/api/tarefa/{id}")]
    Task<IApiResponse> Delete(int id);

    [Post("/api/tarefa/{id}/{status}")]
    Task<IApiResponse> AlterarStatus(int id, StatusTarefa status);
}