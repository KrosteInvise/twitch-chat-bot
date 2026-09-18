using System.Collections.Generic;
using TMPro;

namespace ChatBot
{
    public class ChatMessages
    {
        List<string> chatMessages = new();
        const int MAX_MESSAGES = 150;

        public void AddMessage(string username, string colorHex, string message, TextMeshProUGUI chatText)
        {
            string color = string.IsNullOrEmpty(colorHex) ? "#000000" : colorHex;

            chatMessages.Add($"<{color}>{username}</color>: {message}");
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
