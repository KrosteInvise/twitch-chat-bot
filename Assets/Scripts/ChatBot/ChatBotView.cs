using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
        
        ChatMessages chatMessages;

        public void Init(SignalBus signalBus, ChatMessages chatMessages, ChatBotClient chatBotClient)
        {
            this.chatMessages = chatMessages;
            
            connectButton.onClick.AddListener(() => chatBotClient.Connect(channelInputField.text, botInputField.text));
            disconnectButton.onClick.AddListener(chatBotClient.Disconnect);
            clearChatButton.onClick.AddListener(() =>
            {
                chatText.text = "";
                chatMessages.ClearMessages();
            });
            
            autoHelloButton.onClick.AddListener(() =>
            {
                new AutoHelloResponse().AutoHello(chatBotClient.LastUserPinged, signalBus);
                chatBotClient.LastUserPinged = "";
            });
            
            signalBus.Subscribe<PrintToLocalChatSignal>(OnPrintToChat);
            signalBus.Subscribe<LogToChatSignal>(OnLogToChat);
        }

        void OnLogToChat(LogToChatSignal signal)
        {
            chatMessages.AddLog(signal.Message, chatText);
            chatScrollView.verticalNormalizedPosition = 0f;
        }

        void OnPrintToChat(PrintToLocalChatSignal signal)
        {
            chatMessages.AddMessage(signal.Args, chatText);
            chatScrollView.verticalNormalizedPosition = 0f;
        }
    }
}