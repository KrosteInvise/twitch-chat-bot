using System.Collections.Generic;
using ChatBot;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace WebRequests
{
    public class GetAllPlayersRequest
    {
        public async UniTask<List<PlayerObject>> GetAllRequestAsync(string url)
        {
            var request = UnityWebRequest.Get(url);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await UniTask.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                return JsonConvert.DeserializeObject<List<PlayerObject>>(json);
            }

            Debug.LogError($"Error: {request.error}");
            return null;
        }
    }
}
