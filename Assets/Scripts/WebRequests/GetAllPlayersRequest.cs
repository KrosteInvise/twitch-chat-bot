using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DTO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace WebRequests
{
    public class GetAllPlayersRequest
    {
        public async UniTask<List<PlayerObject>> SendGetAllPlayers()
        {
            var url = "http://localhost:8080/api/players";
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
            return new List<PlayerObject>();
        }
    }
}
