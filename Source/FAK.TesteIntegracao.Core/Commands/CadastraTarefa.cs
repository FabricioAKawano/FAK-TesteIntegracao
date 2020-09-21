using FAK.TesteIntegracao.Core.Models;
using System;

namespace FAK.TesteIntegracao.Core.Commands
{
    public class CadastraTarefa
    {
        public CadastraTarefa(string titulo, Categoria categoria, DateTime prazo)
        {
            Titulo = titulo;
            Categoria = categoria;
            Prazo = prazo;
        }

        public string Titulo { get; }
        public Categoria Categoria { get; }
        public DateTime Prazo { get; }
    }
}
