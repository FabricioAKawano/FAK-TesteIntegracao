using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.Core.Models;
using FAK.TesteIntegracao.Infrastructure;

namespace FAK.TesteIntegracao.Services.Handlers
{
    public class ObtemCategoriaPorIdHandler
    {
        IRepositorioTarefas _repositorioTarefas;

        public ObtemCategoriaPorIdHandler(IRepositorioTarefas repositorioTarefas)
        {
            _repositorioTarefas = repositorioTarefas;
        }
        public Categoria Execute(ObtemCategoriaPorId comando)
        {
            return _repositorioTarefas.ObtemCategoriaPorId(comando.IdCategoria);
        }
    }
}
