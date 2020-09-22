using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.Core.Models;
using FAK.TesteIntegracao.Infrastructure;
using FAK.TesteIntegracao.Services.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var options = new DbContextOptionsBuilder<DbTarefasContext>()
                .UseInMemoryDatabase("DbTarefas")
                .Options;

            var context = new DbTarefasContext(options);
            IRepositorioTarefas repositorioTarefas = new RepositorioTarefas(context);
            var command = new CadastraTarefa("Estudar XUnit", new Categoria("Estudo"), new DateTime(2020, 10, 01));
            var handler = new CadastraTarefaHandler(repositorioTarefas, mockLogger.Object);

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
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var mockRepositorioTarefas = new Mock<IRepositorioTarefas>();
            mockRepositorioTarefas.Setup(t => t.IncluirTarefas(It.IsAny<Tarefa[]>())).Throws(new Exception("Houve um erro na inclusão de tarefas."));
            var repositorioTarefas = mockRepositorioTarefas.Object;
            var command = new CadastraTarefa("Estudar XUnit", new Categoria("Estudo"), new DateTime(2020, 10, 01));
            var handler = new CadastraTarefaHandler(repositorioTarefas, mockLogger.Object);

            //action
            var resultado = handler.Execute(command);

            //assert
            Assert.False(resultado.IsSucceded);
        }

        delegate void CapturaLog(LogLevel logLevel, EventId eventId, object state, Exception exception, Func<object, Exception, string> function);

        [Fact]
        public void DadaTarefaComInfoValidaDeveLogar()
        {
            //arrange
            string TituloTarefaEsperado = "Usar Moq para aprofundar conhecimento de API";
            LogLevel logLevelCapturado = LogLevel.Trace;
            string logMensagemCapturado = string.Empty;
            var command = new CadastraTarefa(TituloTarefaEsperado, new Categoria("Estudo"), new DateTime(2019, 12, 31));
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            CapturaLog captura = (logLevel, eventId, state, exception, function) =>
            {
                logLevelCapturado = logLevel;
                logMensagemCapturado = function(state, exception);
            };
            mockLogger.Setup(l => l.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>())
            ).Callback(captura);
            var mockRepositorioTarefas = new Mock<IRepositorioTarefas>();

            var handler = new CadastraTarefaHandler(mockRepositorioTarefas.Object, mockLogger.Object);

            //action
            var resultado = handler.Execute(command);

            //assert
            Assert.Equal(LogLevel.Information, logLevelCapturado);
            Assert.Contains(TituloTarefaEsperado, logMensagemCapturado);
        }

        [Fact]
        public void QuandoLancarExceptionDeveraLogarMessangemExcption()
        {
            //arrange
            var mensagemDeErroEsperada = "Houve um erro na inclusão de tarefas";
            var excecaoEsperada = new Exception(mensagemDeErroEsperada);

            var comando = new CadastraTarefa("Estudar Xunit", new Categoria("Estudo"), new DateTime(2019, 12, 31));

            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();

            var mock = new Mock<IRepositorioTarefas>();

            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>()))
                .Throws(excecaoEsperada);

            var repo = mock.Object;

            var handler = new CadastraTarefaHandler(repo, mockLogger.Object);

            //act
            CommandResult resultado = handler.Execute(comando);

            //assert
            mockLogger.Verify(l =>
                l.Log(
                    LogLevel.Error, //nível de log => LogError
                    It.IsAny<EventId>(), //identificador do evento
                    It.IsAny<object>(), //objeto que será logado
                    excecaoEsperada,    //exceção que será logada
                    It.IsAny<Func<object, Exception, string>>()
                ), //função que converte objeto+exceção >> string
                Times.Once());
        }
    }
}
