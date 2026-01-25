namespace Signals
{
    public struct LogToChatSignal
    {
        public string Message { get; }

        public LogToChatSignal(string message)
        {
            Message = message;
        }
    }
}