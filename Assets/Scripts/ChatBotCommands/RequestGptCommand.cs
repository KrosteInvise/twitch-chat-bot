using System.Text;
using ChatBot;
using Signals;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace ChatBotCommands
{
    [CreateAssetMenu(fileName = "RequestGptCommand", menuName = "Commands/RequestGptCommand")]
    public class RequestGptCommand : ChatBotCommand
    {
        public override void Execute(CommandContext context)
        {
            if (context.Sender != "kroste_inviser")
                return;

            string question = string.Join(" ", context.Args);
            if (string.IsNullOrEmpty(question))
            {
                context.SignalBus.Fire(new PrintToTwitchChatSignal($"@{context.Sender}, this is not a valid question."));
                return;
            }

            GetGptResponse(context.Sender, question, context.SignalBus);
        }

        async void GetGptResponse(string userName, string question, SignalBus signalBus)
        {
            string apiKey = Secrets.gpt_api_key;
            string url = "https://api.openai.com/v1/chat/completions";

            var jsonData = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "system", content = "Ты — модератор чата Twitch. Твои ответы должны строго соответствовать правилам сообщества Twitch. " +
                                                     "Не используй оскорбления, дискриминацию и запрещенные слова. Если вопрос нарушает правила, ответь: " +
                                                     "'Я не могу на это ответить, это нарушает правила стрима'." },
                    new { role = "user", content = question }
                },
                max_tokens = 100
            };
            
            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData);

            UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(request.downloadHandler.text);
                string aiAnswer = response.choices[0].message.content;
                
                signalBus.Fire(new PrintToTwitchChatSignal($"@{userName}, {aiAnswer}"));
            }
        }
    }
}
