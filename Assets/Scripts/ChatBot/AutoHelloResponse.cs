namespace ChatBot
{
    public class AutoHelloResponse
    {
        public string GetHello(string lastUserPinged)
        {
            if (string.IsNullOrEmpty(lastUserPinged))
                return null;

            return $"@{lastUserPinged} peepoSitHey";
        }
    }
}
