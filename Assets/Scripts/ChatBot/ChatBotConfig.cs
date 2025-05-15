using UnityEngine;

namespace ChatBot
{
	public class ChatBotConfig : ScriptableObject
	{
		[SerializeField]
		string channelNickname;
		
		[SerializeField]
		string passwordFile;

		public string ChannelNickname => channelNickname;
		public string PasswordFile    => passwordFile;
		public string BotNickname => "kroste_inviser";
	}
}