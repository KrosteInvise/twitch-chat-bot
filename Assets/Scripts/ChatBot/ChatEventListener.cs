using UnityEngine.Events;

public static class ChatEventListener
{
    public static UnityAction<string, string> OnCommandReceived;
    public static UnityAction<string> OnGameRespond;
    
    public static void InvokeOnCommandReceived(string sender, string command) {
        OnCommandReceived?.Invoke(sender, command);
    }
    
    public static void InvokeOnGameRespond(string respond) {
        OnGameRespond?.Invoke(respond);
    }
}
