using System.Collections.Generic;

namespace Signals
{
    public struct ReceiveCommandSignal
    {
        public string Sender { get; }
        public string Command { get; }
        public List<string> Args { get; }

        public ReceiveCommandSignal(string sender, string command, List<string> args)
        {
            Sender = sender;
            Command = command;
            Args = args;
        }
    }
}
