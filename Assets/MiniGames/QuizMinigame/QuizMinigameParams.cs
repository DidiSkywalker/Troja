using Minigames;
using UnityEngine;

namespace MiniGames.QuizMinigame
{
    [CreateAssetMenu(menuName = "MinigamesParams/Quiz Params")]
    public class QuizMinigameParams : MinigameParams
    {
        public TextAsset[] quizFiles;
    }
}