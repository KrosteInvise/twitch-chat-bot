using TwitchLib.Client.Events;

namespace Signals
{
    public struct PrintToChatSignal
    {
        public OnMessageReceivedArgs Args { get; set; }

        public PrintToChatSignal(OnMessageReceivedArgs args)
        {
            Args = args;
        }
    }
}