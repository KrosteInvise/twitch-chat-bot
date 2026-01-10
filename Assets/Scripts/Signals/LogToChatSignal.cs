namespace Signals
{
    public struct LogToChatSignal
    {
        public string Message { get; set; }

        public LogToChatSignal(string message)
        {
            Message = message;
        }
    }
}