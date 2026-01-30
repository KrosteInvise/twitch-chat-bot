using ChatBot;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace WebRequests
{
    public class GetByTwitchNameRequest
    {
        public async UniTask<PlayerObject> GetPlayerByTwitchName(string twitchName)
        {
            var url = $"http://localhost:8080/api/players/by-name/{twitchName}";
            var request = UnityWebRequest.Get(url);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await UniTask.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                return JsonConvert.DeserializeObject<PlayerObject>(json);
            }

            Debug.LogError($"Error: {request.error}");
            return null;
        }
    }
}