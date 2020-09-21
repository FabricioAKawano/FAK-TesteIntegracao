namespace FAK.TesteIntegracao.Core.Commands
{
    public class ObtemCategoriaPorId
    {
        public ObtemCategoriaPorId(int idCategoria)
        {
            IdCategoria = idCategoria;
        }

        public int IdCategoria { get; }
    }
}
