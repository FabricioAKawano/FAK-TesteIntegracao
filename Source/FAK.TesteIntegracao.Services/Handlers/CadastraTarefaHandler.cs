using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.Core.Models;
using FAK.TesteIntegracao.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace FAK.TesteIntegracao.Services.Handlers
{
    public class CadastraTarefaHandler
    {
        IRepositorioTarefas _repo;
        ILogger<CadastraTarefaHandler> _logger;

        public CadastraTarefaHandler(IRepositorioTarefas repositorio, ILogger<CadastraTarefaHandler> logger)
        {
            _repo = repositorio;
            _logger = logger;
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
                _logger.LogDebug($"Persistindo a tarefa {tarefa.Titulo}");
                _repo.IncluirTarefas(tarefa);
                return new CommandResult(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new CommandResult(false);
            }
        }
    }
}
