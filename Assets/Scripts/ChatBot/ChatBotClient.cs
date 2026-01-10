using System;
using Signals;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;
using TwitchLib.Unity;
using Zenject;

namespace ChatBot
{
    public class ChatBotClient : MonoBehaviour
    {
        SignalBus signalBus;
        Client client;
        ChatBotConfig config;

        public void Init(SignalBus signalBus, ChatBotConfig config)
        {
            this.config = config;
            this.signalBus = signalBus;
            signalBus.Subscribe<PrintToTwitchChatSignal>(SendMessageToChat);
        }

        public void Connect(ChatBotConfig config)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(config.BotName.ToLower(), Secrets.bot_access_token);
            
            client = new Client();
            client.Initialize(credentials, config.ChannelNickname);
            
            client.OnConnected += OnConnected;
            client.OnJoinedChannel += OnJoinedChannel;
            client.OnLeftChannel += OnLeftChannel;
            client.OnMessageReceived += OnMessageReceived;
            client.OnChatCommandReceived += OnChatCommandReceived;
            client.OnError += OnError;
            client.Connect();
        }
        
        public void Disconnect()
        {
            client.Disconnect();
            client.OnConnected  -= OnConnected;
            client.OnJoinedChannel -= OnJoinedChannel;
            client.OnLeftChannel -= OnLeftChannel;
            client.OnMessageReceived -= OnMessageReceived;
            client.OnChatCommandReceived -= OnChatCommandReceived;
            client.OnError -= OnError;
        }
        
        void SendMessageToChat(PrintToTwitchChatSignal signal)
        {
            client.SendMessage(config.ChannelNickname, signal.Message);
        }
        
        void OnConnected(object sender, OnConnectedArgs args)
        {
            client.JoinChannel(config.ChannelNickname);
            signalBus.Fire(new LogToChatSignal("Bot connected to client\n"));
        }
        
        void OnJoinedChannel(object sender, OnJoinedChannelArgs args)
        {
            signalBus.Fire(new LogToChatSignal($"Bot connected to {config.ChannelNickname}\n"));
        }
        
        void OnLeftChannel(object sender, OnLeftChannelArgs args)
        {
            signalBus.Fire(new LogToChatSignal($"Bot left {config.ChannelNickname} channel\n"));
        }

        void OnMessageReceived(object sender, OnMessageReceivedArgs args)
        {
            signalBus.Fire(new PrintToChatSignal(args));
        }
        
        void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs args)
        {
            signalBus.Fire(new ReceiveCommandSignal(args.Command.ChatMessage.Username, args.Command.CommandText, args.Command.ArgumentsAsList));
        }

        void OnError(object sender, OnErrorEventArgs args)
        {
            signalBus.Fire(new LogToChatSignal($"Error {args.Exception.Message}\n"));
        }
    }
}