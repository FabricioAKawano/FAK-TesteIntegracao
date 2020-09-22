using FAK.TesteIntegracao.Core.Models;
using FAK.TesteIntegracao.Infrastructure;
using FAK.TesteIntegracao.Services.Handlers;
using FAK.TesteIntegracao.WebApp.Controllers;
using FAK.TesteIntegracao.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace FAK.TEsteIntegracao.Tests
{
    public class TarefasControllerEndpointCadastraTarefa
    {
        [Fact]
        public void DadaTarefaComInfoValidasDeveRetornar200()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DbTarefasContext>().UseInMemoryDatabase("DbTarefas").Options;
            var context = new DbTarefasContext(options);
            context.Categorias.Add(new Categoria(20, "Estudo"));
            context.SaveChanges();
            var repositorioTarefas = new RepositorioTarefas(context);
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var controlador = new TarefasController(repositorioTarefas, mockLogger.Object);
            var model = new CadastraTarefaVM
            {
                IdCategoria = 20,
                Prazo = new DateTime(2019, 12, 31),
                Titulo = "Estudar XUnit"
            };

            //action
            var retorno = controlador.EndpointCadastraTarefa(model);

            //assert
            Assert.IsType<OkResult>(retorno); //statu code - 200
        }

        [Fact]
        public void Teste()
        {
            //arrange
            int CategoriaId = 20;
            var mockRepositorioTarefas = new Mock<IRepositorioTarefas>();
            mockRepositorioTarefas.Setup(r => r.ObtemCategoriaPorId(20)).Returns(new Categoria(CategoriaId, "Estudo"));
            mockRepositorioTarefas.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>())).Throws(new Exception("Houve um erro."));
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var controlador = new TarefasController(mockRepositorioTarefas.Object, mockLogger.Object);
            var model = new CadastraTarefaVM
            {
                IdCategoria = CategoriaId,
                Prazo = new DateTime(2019, 12, 31),
                Titulo = "Estudar XUnit"
            };

            //action
            var retorno = controlador.EndpointCadastraTarefa(model);

            //assert
            Assert.IsType<StatusCodeResult>(retorno);
            Assert.Equal(500, (retorno as StatusCodeResult).StatusCode);
        }
    }
}
