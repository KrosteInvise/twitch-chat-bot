using System;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChatBot
{
    public class ChatBotView : MonoBehaviour
    {
        [SerializeField]
        Button connectButton, disconnectButton, clearChatButton, autoHelloButton;

        [SerializeField]
        TextMeshProUGUI chatText;

        [SerializeField]
        ScrollRect chatScrollView;

        [SerializeField]
        TMP_InputField channelInputField, botInputField;

        readonly ChatMessages chatMessages = new();

        public event Action<string, string> ConnectClicked;
        public event Action DisconnectClicked;
        public event Action AutoHelloClicked;

        void Awake()
        {
            connectButton.onClick.AddListener(() =>
                ConnectClicked?.Invoke(channelInputField.text, botInputField.text));
            disconnectButton.onClick.AddListener(() => DisconnectClicked?.Invoke());
            autoHelloButton.onClick.AddListener(() => AutoHelloClicked?.Invoke());
            clearChatButton.onClick.AddListener(() =>
            {
                chatText.text = "";
                chatMessages.ClearMessages();
            });
        }

        public void OnLogToChat(LogToChatSignal signal)
        {
            chatMessages.AddLog(signal.Message, chatText);
            chatScrollView.verticalNormalizedPosition = 0f;
        }

        public void OnPrintToChat(PrintToLocalChatSignal signal)
        {
            chatMessages.AddMessage(signal.Username, signal.ColorHex, signal.Message, chatText);
            chatScrollView.verticalNormalizedPosition = 0f;
        }
    }
}
