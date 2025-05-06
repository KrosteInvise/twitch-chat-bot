namespace ChatBot
{
    public interface IChatBotGame
    {
        void ProceedCommand(string username, string command);
    }
}