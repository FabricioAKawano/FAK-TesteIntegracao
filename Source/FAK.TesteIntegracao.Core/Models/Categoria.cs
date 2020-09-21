namespace FAK.TesteIntegracao.Core.Models
{
    public class Categoria
    {
        public Categoria(string descricao)
        {
            Descricao = descricao;
        }

        public Categoria(int id, string descricao) : this(descricao)
        {
            Id = id;
        }

        /// <summary>
        /// Identificador da categoria
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Descrição da categoria
        /// </summary>
        public string Descricao { get; private set; }
    }
}
