using System.Text;
using Signals;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;
using UniTask = Cysharp.Threading.Tasks.UniTask;

namespace WebRequests
{
    public class AskGptRequest
    {
        public async UniTask GetGptResponse(string userName, string question, SignalBus signalBus)
        {
            string apiKey = Secrets.gpt_api_key;
            string url = "https://api.openai.com/v1/chat/completions";

            var jsonData = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "system", content = "Ты — модератор чата Twitch, но добрый, как близкий друг. Используй поиск для более точной информации. " +
                                                     "Не спрашивай уточняющие вопросы, просто давай ответ. " +
                                                     "Не используй оскорбления, дискриминацию и запрещенные слова. Укладывайся в 450 символов максимум."},
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
            
            Debug.LogError("Error: " + request.error);
        }
    }
}