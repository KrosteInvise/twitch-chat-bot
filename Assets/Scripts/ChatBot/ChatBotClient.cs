using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models.Common;
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
            repeatButton.onClick.AddListener(SpamCommand);
            ChatEventListener.OnGameRespond += SendMessageToChat;
        }

        void Connect()
        {
            Application.runInBackground = true;
            ConnectionCredentials credentials = new ConnectionCredentials(config.BotNickname.ToLower(), File.ReadAllText(config.PasswordFile));
            client = new Client();
            client.Initialize(credentials, config.ChannelNickname);
            
            chatText.text += "Connecting...\n";
            
            client.OnConnected += OnConnected;
            client.OnDisconnected += OnDisconnected;
            client.OnJoinedChannel += OnJoinedChannel;
            client.OnMessageReceived += OnMessageReceived;
            client.OnChatCommandReceived += OnChatCommandReceived;
            client.OnError += OnError;
            
            client.Connect();
        }
        
        void Disconnect()
        {
            client.Disconnect();
            client.OnConnected     -= OnConnected;
            client.OnDisconnected -= OnDisconnected;
            client.OnJoinedChannel -= OnJoinedChannel;
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
            chatText.text += $"<color=\"red\">Error {args.Exception.Message}</color>\n";
        }
        
        void SpamCommand()
        {
            List<string> arguments = Helpers.ParseQuotesAndNonQuotes(repeatTextInputField.text);
            string iterations = arguments.First();
            string message = arguments.ElementAtOrDefault(1);

            if (arguments.Count != 2 || !int.TryParse(iterations, out int repeatCount) || repeatCount <= 0 || repeatCount > 8)
            {
                Debug.LogError("<color=\"red\">Something went wrong when trying to spam");
                return;
            }
            
            for (int i = 0; i < repeatCount; i++)
                client.SendMessage(config.ChannelNickname, message);
        }
    }
}

