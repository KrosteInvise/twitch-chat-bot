using System.Collections.Generic;
using UnityEngine.Events;

public static class ChatEventListener
{
    public static UnityAction<string, string, List<string>> OnCommandReceived;
    public static UnityAction<string> OnGameRespond;
    
    public static void InvokeOnCommandReceived(string sender, string command, List<string> args) {
        OnCommandReceived?.Invoke(sender, command, args);
    }
    
    public static void InvokeOnGameRespond(string respond) {
        OnGameRespond?.Invoke(respond);
    }
}
