using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.Core.Models;
using FAK.TesteIntegracao.Infrastructure;
using Microsoft.Extensions.Logging;

namespace FAK.TesteIntegracao.Services.Handlers
{
    public class CadastraTarefaHandler
    {
        private readonly IRepositorioTarefas _repositorioTarefas;
        private readonly ILogger<CadastraTarefaHandler> _logger;

        public CadastraTarefaHandler(IRepositorioTarefas repositorioTarefas)
        {
            _repositorioTarefas = repositorioTarefas;
            _logger = new LoggerFactory().CreateLogger<CadastraTarefaHandler>();
        }

        public CommandResult Execute(CadastraTarefa comando)
        {
            try
            {
                var tarefa = new Tarefa
                    (
                        id: 0,
                        titulo: comando.Titulo,
                        prazo: comando.Prazo,
                        categoria: comando.Categoria,
                        concluidaEm: null,
                        status: StatusTarefa.Criada
                    );
                _logger.LogDebug("Persistindo a tarefa...");
                _repositorioTarefas.IncluirTarefas(tarefa);
                return new CommandResult(true);
            }
            catch
            {
                return new CommandResult(false);
            }
        }
    }
}
