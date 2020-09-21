using FAK.TesteIntegracao.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FAK.TesteIntegracao.Infrastructure
{
    public class RepositorioTarefas : IRepositorioTarefas
    {
        DbTarefasContext _context;

        public RepositorioTarefas(DbTarefasContext context)
        {
            _context = context;
        }

        public void AtualizarTarefas(params Tarefa[] tarefas)
        {
            _context.Tarefas.UpdateRange(tarefas);
            _context.SaveChanges();
        }

        public void ExcluirTarefas(params Tarefa[] tarefas)
        {
            _context.Tarefas.RemoveRange(tarefas);
            _context.SaveChanges();
        }

        public void IncluirTarefas(params Tarefa[] tarefas)
        {
            _context.Tarefas.AddRange(tarefas);
            _context.SaveChanges();
        }

        public Categoria ObtemCategoriaPorId(int id)
        {
            return _context.Categorias.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Tarefa> ObtemTarefas(Func<Tarefa, bool> filtro)
        {
            return _context.Tarefas.Where(filtro);
        }
    }
}
