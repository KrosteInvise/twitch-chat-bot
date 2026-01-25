using UnityEngine;
using UnityEngine.Networking;

namespace WebRequests
{
    public class GetAllPlayersRequest
    {
        public async void GetAllRequestAsync(string url)
        {
            var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
            await request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                var a = request.downloadHandler.text;
                Debug.Log(a);
            }
            else
            {
                Debug.Log("Error: " + request.error);
            }
        }
    }
}
