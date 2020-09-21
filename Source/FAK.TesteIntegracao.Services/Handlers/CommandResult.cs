namespace FAK.TesteIntegracao.Services.Handlers
{
    public class CommandResult
    {
        public CommandResult(bool isSucceded)
        {
            IsSucceded = isSucceded;
        }

        public bool IsSucceded { get; }
    }
}
