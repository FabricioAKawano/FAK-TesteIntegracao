using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.Infrastructure;
using FAK.TesteIntegracao.Services.Handlers;
using Moq;
using Xunit;

namespace FAK.TEsteIntegracao.Tests
{
    public class ObtemCategoriaPorIdHandlerExecute
    {
        [Fact]
        public void DeveChamarObtemCategoriaPorIdUmaUnicaVez()
        {
            //arrange
            var categoraiId = 10;
            var mock = new Mock<IRepositorioTarefas>();
            var repositorioTarefas = mock.Object;

            var command = new ObtemCategoriaPorId(categoraiId);
            var handle = new ObtemCategoriaPorIdHandler(repositorioTarefas);

            //action
            handle.Execute(command);

            //assert
            mock.Verify(r => r.ObtemCategoriaPorId(categoraiId), Times.Once());
        }
    }
}
