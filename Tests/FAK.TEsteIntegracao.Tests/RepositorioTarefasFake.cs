using FAK.TesteIntegracao.Core.Models;
using FAK.TesteIntegracao.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FAK.TEsteIntegracao.Tests
{
    public class RepositorioTarefasFake : IRepositorioTarefas
    {
        //Repositorio inutilisado, pois foi substituido pelo InMemoryDatabase do EntityFrameworkCore
        List<Tarefa> table_Tarefas = new List<Tarefa>();
        public void AtualizarTarefas(params Tarefa[] tarefas)
        {
            throw new NotImplementedException();
        }

        public void ExcluirTarefas(params Tarefa[] tarefas)
        {
            throw new NotImplementedException();
        }

        public void IncluirTarefas(params Tarefa[] tarefas)
        {
            table_Tarefas.AddRange(tarefas);
        }

        public Categoria ObtemCategoriaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tarefa> ObtemTarefas(Func<Tarefa, bool> filtro)
        {
            return table_Tarefas.Where(filtro);
        }
    }
}
