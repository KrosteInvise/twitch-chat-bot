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
        string channelName;
        string botName;
        string lastUserPinged;

        [Inject]
        void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void Connect(string channelName, string botName)
        {
            this.channelName = channelName;
            this.botName = botName;
            ConnectionCredentials credentials = new ConnectionCredentials(botName.ToLower(), Secrets.bot_access_token);

            client = new Client();
            client.Initialize(credentials, channelName);

            client.OnConnected += OnConnected;
            client.OnDisconnected += OnDisconnected;
            client.OnJoinedChannel += OnJoinedChannel;
            client.OnLeftChannel += OnLeftChannel;
            client.OnMessageReceived += OnMessageReceived;
            client.OnChatCommandReceived += OnChatCommandReceived;
            client.OnError += OnError;
            client.Connect();
        }

        public void Disconnect()
        {
            if (client == null)
                return;

            client.Disconnect();
            client.OnConnected -= OnConnected;
            client.OnDisconnected -= OnDisconnected;
            client.OnJoinedChannel -= OnJoinedChannel;
            client.OnLeftChannel -= OnLeftChannel;
            client.OnMessageReceived -= OnMessageReceived;
            client.OnChatCommandReceived -= OnChatCommandReceived;
            client.OnError -= OnError;
        }

        public void SendAutoHello()
        {
            string message = new AutoHelloResponse().GetHello(lastUserPinged);
            if (string.IsNullOrEmpty(message))
                return;

            lastUserPinged = "";
            signalBus.Fire(new PrintToTwitchChatSignal(message));
        }

        public void SendMessageToChat(PrintToTwitchChatSignal signal)
        {
            if (client == null)
                return;

            client.SendMessage(channelName, signal.Message);
        }

        void OnBeingPinged(string username, string message)
        {
            if (message.Contains($"{botName}", StringComparison.OrdinalIgnoreCase)) lastUserPinged = username;
        }

        void OnConnected(object sender, OnConnectedArgs args)
        {
            client.JoinChannel(channelName);
            signalBus.Fire(new LogToChatSignal("Bot connected to client"));
        }

        void OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            client.LeaveChannel(channelName);
            signalBus.Fire(new LogToChatSignal("Bot disconnected"));
        }

        void OnJoinedChannel(object sender, OnJoinedChannelArgs args)
        {
            signalBus.Fire(new LogToChatSignal($"Bot connected to {args.Channel}"));
        }

        void OnLeftChannel(object sender, OnLeftChannelArgs args)
        {
            signalBus.Fire(new LogToChatSignal($"Bot left {args.Channel} channel"));
        }

        void OnMessageReceived(object sender, OnMessageReceivedArgs args)
        {
            OnBeingPinged(args.ChatMessage.Username, args.ChatMessage.Message);
            signalBus.Fire(new PrintToLocalChatSignal(
                args.ChatMessage.Username,
                args.ChatMessage.ColorHex,
                args.ChatMessage.Message));
        }

        void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs args)
        {
            signalBus.Fire(new ReceiveCommandSignal(args.Command.ChatMessage.Username, args.Command.CommandText, args.Command.ArgumentsAsList));
        }

        void OnError(object sender, OnErrorEventArgs args)
        {
            signalBus.Fire(new LogToChatSignal($"Error {args.Exception.Message}"));
        }
    }
}
