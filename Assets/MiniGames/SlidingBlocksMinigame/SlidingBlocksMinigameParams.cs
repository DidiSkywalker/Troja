using Minigames;
using UnityEngine;

namespace MiniGames.SlidingBlocksMinigame
{
    
    [CreateAssetMenu(menuName = "MinigamesParams/Sliding Blocks Params")]
    public class SlidingBlocksMinigameParams : MinigameParams
    {
        public Texture2D image;
        public int blocksPerRow;
        public int shuffleMoves;
    }
}