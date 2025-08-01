using System.Collections.Generic;
using UnityEngine.Events;

namespace ChatBot
{
    public static class ChatEventMediator
    {
        public static UnityAction<string, string, List<string>> OnCommandReceived;
        public static UnityAction<string> OnRespond;
        
        public static void InvokeOnCommandReceived(string sender, string command, List<string> args) {
            OnCommandReceived?.Invoke(sender, command, args);
        }
        
        public static void InvokeRespond(string respond) {
            OnRespond?.Invoke(respond);
        }
    }

}
