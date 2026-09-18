namespace Signals
{
    public struct PrintToLocalChatSignal
    {
        public string Username { get; }
        public string ColorHex { get; }
        public string Message { get; }

        public PrintToLocalChatSignal(string username, string colorHex, string message)
        {
            Username = username;
            ColorHex = colorHex;
            Message = message;
        }
    }
}
