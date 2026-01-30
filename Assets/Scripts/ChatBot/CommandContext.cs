using System.Collections.Generic;
using Zenject;

namespace ChatBot
{
    public class CommandContext
    {
        public string Sender { get; set; }
        public List<string> Args { get; set; }
        public SignalBus SignalBus { get; set; }
    }
}