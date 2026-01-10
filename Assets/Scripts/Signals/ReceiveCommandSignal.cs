using System.Collections.Generic;

namespace Signals
{
    public struct ReceiveCommandSignal
    {
        public string Sender { get; set; }
        public string Command { get; set; }
        public List<string> Args { get; set; }

        public ReceiveCommandSignal(string sender, string command, List<string> args)
        {
            Sender = sender;
            Command = command;
            Args = args;
        }
    }
}
