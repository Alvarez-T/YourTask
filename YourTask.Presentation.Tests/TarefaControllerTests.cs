using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using YourTask.Domain.Models;
using YourTask.Domain.Validations;
using YourTask.Infrastructure;
using YourTask.Presentation.Controllers;

namespace YourTask.Presentation.Tests
{
    public class TarefaControllerTests
    {
        private readonly Mock<ITarefaRepository> _repositoryMock;
        private readonly TarefaController _controller;
        private readonly IValidator<Tarefa> _validator;

        public TarefaControllerTests()
        {
            // Configura o mock do repositório e instancia o controlador
            _repositoryMock = new Mock<ITarefaRepository>();
            _validator = new TarefaValidator();
            _controller = new TarefaController(_repositoryMock.Object);
        }

        #region GetAll

        [Fact]
        public async Task GetAll_DeveRetornarOk_ComListaDeTarefas()
        {
            // Arrange
            var tarefas = new List<Tarefa>
            {
                new Tarefa { Id = 1, Titulo = "Tarefa 1" },
                new Tarefa { Id = 2, Titulo = "Tarefa 2" }
            };
            _repositoryMock.Setup(r => r.ObterTodasTarefas()).ReturnsAsync(tarefas);

            // Act
            var resultado = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            Assert.Equal(tarefas, okResult.Value);
        }

        #endregion
        #region GetById

        [Fact]
        public async Task GetById_IdExistente_DeveRetornarOk_ComTarefa()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa Teste" };
            _repositoryMock.Setup(r => r.ObterTarefaPorId(1)).ReturnsAsync(tarefa);

            // Act
            var resultado = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            Assert.Equal(tarefa, okResult.Value);
        }

        [Fact]
        public async Task GetById_IdInexistente_DeveRetornarNotFound()
        {
            // Arrange
            _repositoryMock.Setup(r => r.ObterTarefaPorId(99)).ReturnsAsync((Tarefa?)null);

            // Act
            var resultado = await _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado.Result);
        }

        #endregion

        #region GetPorPeriodo

        [Fact]
        public async Task GetPorPeriodo_PeriodoValido_DeveRetornarOk_ComTarefas()
        {
            // Arrange
            var tarefas = new List<Tarefa>
            {
                new Tarefa { Id = 1, Titulo = "Tarefa 1" },
                new Tarefa { Id = 2, Titulo = "Tarefa 2" }
            };

            DateTime dataInicial = DateTime.Today.AddDays(-1);


            _repositoryMock.Setup(r => r.ObtemTarefasPorPeriodo(dataInicial, null))
                           .ReturnsAsync(tarefas);

            // Act
            var resultado = await _controller.GetPorPeriodo(null, dataInicial);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            Assert.Equal(tarefas, okResult.Value);
        }

        #endregion

        #region Create

        [Fact]
        public async Task Create_TarefaValida_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, Titulo = "Nova Tarefa" };
            _repositoryMock.Setup(r => r.CriarTarefa(tarefa)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.Create(tarefa);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultado);
            Assert.Equal(nameof(_controller.GetAll), createdResult.ActionName);
            Assert.Equal(tarefa, createdResult.Value);
        }

        [Fact]
        public async Task Create_QuandoOcorreExcecao_DeveRetornarStatusCode500()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, Titulo = "Nova Tarefa" };
            _repositoryMock.Setup(r => r.CriarTarefa(tarefa))
                           .ThrowsAsync(new Exception("Erro na criação"));

            // Act
            var resultado = await _controller.Create(tarefa);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(500, objectResult.StatusCode);
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_TarefaValida_DeveRetornarNoContent()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 1, Titulo = "Atualizar Tarefa" };
            _repositoryMock.Setup(r => r.AtualizarTarefa(tarefa)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.Update(tarefa);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        #endregion

        #region Delete

        [Fact]
        public async Task Delete_IdExistente_DeveRetornarNoContent()
        {
            // Arrange
            int id = 1;
            _repositoryMock.Setup(r => r.DeletarTarefa(id)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task Delete_IdInexistente_DeveRetornarNotFound()
        {
            // Arrange
            int id = 99;
            _repositoryMock.Setup(r => r.DeletarTarefa(id))
                           .ThrowsAsync(new KeyNotFoundException("Não encontrado"));

            // Act
            var resultado = await _controller.Delete(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
            Assert.Equal("Não encontrado", notFoundResult.Value);
        }

        #endregion

        #region AlterarStatus

        [Fact]
        public async Task AlterarStatus_DadosValidos_DeveRetornarNoContent()
        {
            // Arrange
            int id = 1;
            var status = StatusTarefa.Concluida; 
            _repositoryMock.Setup(r => r.AtualizarStatusTarefa(id, status)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _controller.AlterarStatus(id, status);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task AlterarStatus_IdInexistente_DeveRetornarNotFound()
        {
            // Arrange
            int id = 99;
            var status = StatusTarefa.Pendente;
            _repositoryMock.Setup(r => r.AtualizarStatusTarefa(id, status))
                           .ThrowsAsync(new KeyNotFoundException("Não encontrado"));

            // Act
            var resultado = await _controller.AlterarStatus(id, status);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
            Assert.Equal("Não encontrado", notFoundResult.Value);
        }

        [Fact]
        public async Task AlterarStatus_QuandoOcorreExcecao_DeveRetornarStatusCode500()
        {
            // Arrange
            int id = 1;
            var status = StatusTarefa.Pendente;
            _repositoryMock.Setup(r => r.AtualizarStatusTarefa(id, status))
                           .ThrowsAsync(new Exception("Erro ao atualizar status"));

            // Act
            var resultado = await _controller.AlterarStatus(id, status);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(500, objectResult.StatusCode);
        }

        #endregion
    }
}
