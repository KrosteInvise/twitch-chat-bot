using System.Collections.Generic;
using TMPro;
using TwitchLib.Client.Events;

namespace ChatBot
{
    public class ChatMessages
    {
        List<string> chatMessages = new();
        const int MAX_MESSAGES = 150;
        
        public void AddMessage(OnMessageReceivedArgs args, TextMeshProUGUI chatText)
        {
            string color = args.ChatMessage.ColorHex;
            
            if (color == "")
                color = "#FFFFFF";
            
            chatMessages.Add($"<{color}>{args.ChatMessage.Username}</color>: {args.ChatMessage.Message}");
            if (chatMessages.Count > MAX_MESSAGES)
                chatMessages.RemoveAt(0);

            chatText.text = string.Join("\n", chatMessages);
        }
    }
}