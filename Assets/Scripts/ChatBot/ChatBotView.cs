using Signals;
using TMPro;
using TwitchLib.Client.Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ChatBot
{
    public class ChatBotView : MonoBehaviour
    {
        [SerializeField]
        Button connectButton, disconnectButton;
        
        [SerializeField]
        TextMeshProUGUI chatText;
        
        [SerializeField]
        ScrollRect chatScrollView;
        
        ChatMessages chatMessages;

        public void Init(SignalBus signalBus, ChatMessages chatMessages, ChatBotClient chatBotClient, ChatBotConfig config)
        {
            this.chatMessages = chatMessages;
            
            connectButton.onClick.AddListener(() => chatBotClient.Connect(config));
            disconnectButton.onClick.AddListener(chatBotClient.Disconnect);
            signalBus.Subscribe<PrintToChatSignal>(OnPrintToChat);
            signalBus.Subscribe<LogToChatSignal>(OnLogToChat);
        }

        void OnLogToChat(LogToChatSignal signal)
        {
            chatMessages.AddLog(signal.Message, chatText);
            chatScrollView.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }

        void OnPrintToChat(PrintToChatSignal signal)
        {
            chatMessages.AddMessage(signal.Args, chatText);
            chatScrollView.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
    }
}