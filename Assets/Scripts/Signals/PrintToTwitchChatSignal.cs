namespace Signals
{
    public struct PrintToTwitchChatSignal
    {
        public string Message { get; set; }

        public PrintToTwitchChatSignal(string message)
        {
            Message = message;
        }
    }
}
