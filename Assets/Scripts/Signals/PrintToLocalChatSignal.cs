using TwitchLib.Client.Events;

namespace Signals
{
    public struct PrintToLocalChatSignal
    {
        public OnMessageReceivedArgs Args { get; }

        public PrintToLocalChatSignal(OnMessageReceivedArgs args)
        {
            Args = args;
        }
    }
}