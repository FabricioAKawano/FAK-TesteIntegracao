using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.Core.Models;
using FAK.TesteIntegracao.Infrastructure;
using System.Linq;

namespace FAK.TesteIntegracao.Services.Handlers
{
    public class GerenciaPrazoDasTarefasHandler
    {
        private readonly IRepositorioTarefas _repositorioTarefas;

        public GerenciaPrazoDasTarefasHandler(IRepositorioTarefas repositorioTarefas)
        {
            _repositorioTarefas = repositorioTarefas;
        }

        public void Execute(GerenciaPrazoDasTarefas comando)
        {
            var agora = comando.DataHoraAtual;

            //pegar todas as tarefas não concluídas que passaram do prazo
            var tarefas = _repositorioTarefas
                .ObtemTarefas(t => t.Prazo <= agora && t.Status != StatusTarefa.Concluida)
                .ToList();

            //atualizá-las com status Atrasada
            tarefas.ForEach(t => t.Status = StatusTarefa.EmAtraso);

            //salvar tarefas
            _repositorioTarefas.AtualizarTarefas(tarefas.ToArray());
        }
    }
}
