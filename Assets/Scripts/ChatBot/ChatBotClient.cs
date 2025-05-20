using ChatBotCommands;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;
using TwitchLib.Unity;

namespace ChatBot
{
    public class ChatBotClient : MonoBehaviour
    {
        [SerializeField]
        Button connectButton, disconnectButton, repeatButton;
        
        [SerializeField]
        TextMeshProUGUI chatText;
        
        [SerializeField]
        TMP_InputField repeatTextInputField;

        Client client;
        ChatBotConfig config;
        ChatMessages chatMessages = new();

        public void Init(ChatBotConfig config)
        {
            this.config = config;
            connectButton.onClick.AddListener(Connect);
            disconnectButton.onClick.AddListener(Disconnect);
            repeatButton.onClick.AddListener(() => new Repeat().RepeatExecute(client, config, repeatTextInputField));
            ChatEventListener.OnGameRespond += SendMessageToChat;
        }

        void Connect()
        {
            Application.runInBackground = true;
            ConnectionCredentials credentials = new ConnectionCredentials(config.BotName.ToLower(), Secrets.bot_access_token);
            client = new Client();
            client.Initialize(credentials, config.ChannelNickname);
            
            chatText.text += "Connecting...\n";
            
            client.OnConnected += OnConnected;
            client.OnDisconnected += OnDisconnected;
            client.OnJoinedChannel += OnJoinedChannel;
            client.OnLeftChannel += OnLeftChannel;
            client.OnMessageReceived += OnMessageReceived;
            client.OnChatCommandReceived += OnChatCommandReceived;
            client.OnError += OnError;
            
            client.Connect();
        }
        
        void Disconnect()
        {
            client.Disconnect();
            client.OnConnected  -= OnConnected;
            client.OnDisconnected -= OnDisconnected;
            client.OnJoinedChannel -= OnJoinedChannel;
            client.OnLeftChannel -= OnLeftChannel;
            client.OnMessageReceived -= OnMessageReceived;
            client.OnChatCommandReceived -= OnChatCommandReceived;
            client.OnError -= OnError;
        }
        
        void OnConnected(object sender, OnConnectedArgs args)
        {
            client.JoinChannel(config.ChannelNickname);
            chatText.text += "Bot connected to client\n";
        }
        
        void OnDisconnected(object sender, OnDisconnectedEventArgs args)
        {
            client.LeaveChannel(config.ChannelNickname);
            chatText.text += $"Bot disconnected from {config.ChannelNickname}\n";
        }
        
        void OnJoinedChannel(object sender, OnJoinedChannelArgs args)
        {
            chatText.text += $"Bot connected to {config.ChannelNickname}\n";
        }
        
        void OnLeftChannel(object sender, OnLeftChannelArgs args)
        {
            chatText.text += $"Bot left {config.ChannelNickname} channel\n";
        }

        void SendMessageToChat(string message)
        {
            client.SendMessage(config.ChannelNickname, message);
        }

        void OnMessageReceived(object sender, OnMessageReceivedArgs args)
        {
            chatMessages.AddMessage(args, chatText);
        }
        
        void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs args)
        {
            ChatEventListener.InvokeOnCommandReceived(args.Command.ChatMessage.Username, args.Command.CommandText, args.Command.ArgumentsAsList);
        }

        void OnError(object sender, OnErrorEventArgs args)
        {
            chatText.text += $"Error {args.Exception.Message}</color>\n";
        }
    }
}

