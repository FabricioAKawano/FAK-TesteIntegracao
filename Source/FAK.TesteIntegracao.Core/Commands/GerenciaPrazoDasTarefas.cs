using System;

namespace FAK.TesteIntegracao.Core.Commands
{
    public class GerenciaPrazoDasTarefas
    {
        public GerenciaPrazoDasTarefas(DateTime dataHoraAtual)
        {
            DataHoraAtual = dataHoraAtual;
        }

        public DateTime DataHoraAtual { get; }
    }
}
