using System.Collections.Generic;

namespace ChatBot
{
    public class CommandContext
    {
        public string Sender { get; set; }
        public List<string> Args { get; set; }
    }
}
