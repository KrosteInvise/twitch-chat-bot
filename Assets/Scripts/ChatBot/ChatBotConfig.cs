using UnityEngine;

namespace ChatBot
{
	public class ChatBotConfig : ScriptableObject
	{
		[SerializeField]
		string channelNickname;

		[SerializeField]
		string botName;

		public string ChannelNickname => channelNickname;

		public string BotName => botName;
	}
}