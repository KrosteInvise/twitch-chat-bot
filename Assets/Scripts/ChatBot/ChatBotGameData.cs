using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using WebRequests;

namespace ChatBot
{
    public static class ChatBotGameData
    {
        public static async UniTask<PlayersDataBase> LoadAsync()
        {
            string url = "http://localhost:8080/api/players";
            var request = new GetAllPlayersRequest();
            
            List<PlayerObject> players = await request.GetAllRequestAsync(url);
            
            PlayersDataBase dataBase = new PlayersDataBase();
            dataBase.PlayersDataList = players;
            
            return dataBase;
        }
    }
}

