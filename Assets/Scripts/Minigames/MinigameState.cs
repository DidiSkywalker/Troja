namespace Minigames
{
    public class MinigameState
    {
        public static readonly MinigameState Instance = new();

        public static bool IsMinigameRunning()
        {
            return Instance.ActiveMinigame != null;
        }

        public MinigameSO ActiveMinigame;
        private MinigameState() {}
    }
}