using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.Core.Models;
using FAK.TesteIntegracao.Infrastructure;
using FAK.TesteIntegracao.Services.Handlers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace FAK.TEsteIntegracao.Tests
{
    public class CadastraTarefaHandlerExecute
    {
        [Fact]
        public void DadaTarefaComInfoValidasIncluiNoBancoDados()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DbTarefasContext>()
                .UseInMemoryDatabase("DbTarefas")
                .Options;

            var context = new DbTarefasContext(options);
            IRepositorioTarefas repositorioTarefas = new RepositorioTarefas(context);
            var command = new CadastraTarefa("Estudar XUnit", new Categoria("Estudo"), new DateTime(2020, 10, 01));
            var handler = new CadastraTarefaHandler(repositorioTarefas);

            //Action
            handler.Execute(command);
            var tarefa = repositorioTarefas.ObtemTarefas(t => t.Titulo.Equals("Estudar XUnit", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            //Assert
            Assert.NotNull(tarefa);
        }

        [Fact]
        public void QuandoLancarExceptionIsSuccededFalse()
        {
            //arrange
            var mock = new Mock<IRepositorioTarefas>();
            mock.Setup(t => t.IncluirTarefas(It.IsAny<Tarefa[]>())).Throws(new Exception("Erro ao inserir tarefas."));
            var repositorioTarefas = mock.Object;
            var command = new CadastraTarefa("Estudar XUnit", new Categoria("Estudo"), new DateTime(2020, 10, 01));
            var handler = new CadastraTarefaHandler(repositorioTarefas);

            //action
            var resultado = handler.Execute(command);

            //assert
            Assert.False(resultado.IsSucceded);
        }
    }
}
