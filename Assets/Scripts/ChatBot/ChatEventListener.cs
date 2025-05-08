using UnityEngine.Events;

public sealed class ChatEventListener
{
    public UnityAction<string, string> OnCommandReceived;
    
    public void InvokeOnCommandReceived(string sender, string command)
    {
        OnCommandReceived?.Invoke(sender, command);
    }
}
