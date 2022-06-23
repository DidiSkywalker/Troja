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
        public MinigameParams MinigameParams;
        private MinigameState() {}

        public T GetParams<T>() where T : MinigameParams
        {
            if (MinigameParams is T @params)
            {
                return @params;
            }

            return null;
        }
    }
}