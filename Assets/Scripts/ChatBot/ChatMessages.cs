using System.Collections.Generic;
using UnityEngine.UI;

namespace ChatBot
{
    public class ChatMessages
    {
        List<string> chatMessages = new();
        const int MAX_MESSAGES = 150;
        
        public void AddMessage(string sender, string message, Text chatText)
        {
            chatMessages.Add($"{sender}: {message}");
            if (chatMessages.Count > MAX_MESSAGES)
                chatMessages.RemoveAt(0);

            chatText.text = string.Join("\n", chatMessages);
        }
    }
}
