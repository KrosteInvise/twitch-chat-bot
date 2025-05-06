using UnityEngine;

namespace ChatBot
{
	public class ChatBotConfig : ScriptableObject
	{
		[SerializeField]
		string botNickname;

		[SerializeField]
		string passwordFile;

		[SerializeField]
		string channelNickname;

		public string BotNickname     => botNickname;
		public string PasswordFile    => passwordFile;
		public string ChannelNickname => channelNickname;
	}
}