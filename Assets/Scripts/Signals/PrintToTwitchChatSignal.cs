namespace Signals
{
    public struct PrintToTwitchChatSignal
    {
        public string Message { get; }

        public PrintToTwitchChatSignal(string message)
        {
            Message = message;
        }
    }
}
