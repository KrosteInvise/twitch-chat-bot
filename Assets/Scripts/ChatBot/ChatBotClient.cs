using System.IO;
using System.Linq;
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
        ChatBotConfig config;
        
        [SerializeField]
        Button connectButton, disconnectButton;
        
        [SerializeField]
        Text chatText;
        
        Client client;
        IChatBotGame chatBotGame; 
        ChatManager chatManager = new();
        
        public void Init()
        {
            connectButton.onClick.AddListener(Connect);
            disconnectButton.onClick.AddListener(Disconnect);
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
        
        void OnConnected(object sender, OnConnectedArgs e)
        {
            client.JoinChannel(config.ChannelNickname);
            chatText.text += "Bot connected to client\n";
        }
        
        void OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            client.LeaveChannel(config.ChannelNickname);
            chatText.text += $"Bot disconnected from {config.ChannelNickname}\n";
        }
        
        void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            chatText.text += $"Bot connected to {config.ChannelNickname}\n";
        }
        
        void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            chatManager.AddMessage(e.ChatMessage.Username, e.ChatMessage.Message, chatText);
        }
        void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            if (e.Command.ChatMessage.Username == config.BotNickname && e.Command.CommandText.StartsWith("r"))
                SpamCommand(e);
        }

        void OnError(object sender, OnErrorEventArgs e)
        {
            chatText.text += $"Error {e.Exception.Message}\n";
        }

        void SpamCommand(OnChatCommandReceivedArgs e)
        {
            var arguments = e.Command.ArgumentsAsList;
            string iterations = arguments.First();
            string message = arguments.ElementAtOrDefault(1);

            if (arguments.Count < 2)
            {
                client.SendMessage(e.Command.ChatMessage.Channel,$"{e.Command.ChatMessage.Username}, насрал в аргументы");
                return;
            }

            if (!int.TryParse(iterations, out int repeatCount) || repeatCount <= 0)
            {
                client.SendMessage(e.Command.ChatMessage.Channel, $"{e.Command.ChatMessage.Username}, насрал в число");
                return;
            }

            if (repeatCount > 10)
            {
                client.SendMessage(e.Command.ChatMessage.Channel, "Too much iterations (> 10)");
                return;
            }
            
            for (int i = 0; i < repeatCount; i++)
                client.SendMessage(e.Command.ChatMessage.Channel, message);
        }
    }
}

