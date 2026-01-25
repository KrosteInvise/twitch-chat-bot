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
                color = "#000000";
            
            chatMessages.Add($"<{color}>{args.ChatMessage.Username}</color>: {args.ChatMessage.Message}");
            if (chatMessages.Count > MAX_MESSAGES)
                chatMessages.RemoveAt(0);

            chatText.text = string.Join("\n", chatMessages);
        }

        public void AddLog(string message, TextMeshProUGUI chatText)
        {
            chatMessages.Add($"<#ff0000>{message}</color>");
            
            if (chatMessages.Count > MAX_MESSAGES)
                chatMessages.RemoveAt(0);
            
            chatText.text = string.Join("\n", chatMessages);
        }

        public void ClearMessages()
        {
            chatMessages.Clear();
        }
    }
}