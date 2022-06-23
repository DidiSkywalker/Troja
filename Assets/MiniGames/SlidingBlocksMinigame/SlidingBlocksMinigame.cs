using System;
using System.Collections.Generic;
using System.Linq;
using Events.Channels;
using Minigames;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGames.SlidingBlocksMinigame
{
    public class SlidingBlocksMinigame : MonoBehaviour
    {
        public new Camera camera;
        public Texture2D image;
        public int blocksPerLine = 4;
        public int shuffleLength = 2;
        public float defaultMoveDuration = .2f;
        public float shuffleMoveDuration = .1f;
        public MinigameSO minigameSO;
        public MinigameEventChannelSO minigameSuccessChannel;
        public TextMeshPro successText;

        private enum PuzzleState { Solved, Shuffling, InPlay };
        private PuzzleState _state;
        private long _solvedAt = -1;
        private const long AfterSolvedDelay = 3;

        private Block _emptyBlock;
        private Block[,] _blocks;
        private Queue<Block> _inputs;
        private bool _blockIsMoving;
        private int _shuffleMovesRemaining;
        private Vector2Int _prevShuffleOffset;

        private void Start()
        {
            var parms = MinigameState.Instance.GetParams<SlidingBlocksMinigameParams>();
            if (parms != null)
            {
                blocksPerLine = parms.blocksPerRow;
                shuffleLength = parms.shuffleMoves;
                image = parms.image;
            }
            CreatePuzzle();
            StartShuffle();
        }

        private void Update()
        {
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();
            if (_solvedAt > 0 && _solvedAt + AfterSolvedDelay < now)
            {
                minigameSuccessChannel.RaiseEvent(minigameSO);
                _solvedAt = -1;
            }
        }

        private void CreatePuzzle()
        {
            _blocks = new Block[blocksPerLine, blocksPerLine];
            var imageSlices = ImageSlicer.GetSlices(image, blocksPerLine);
            for (var y = 0; y < blocksPerLine; y++)
            {
                for (var x = 0; x < blocksPerLine; x++)
                {
                    var blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y);
                    blockObject.transform.parent = transform;
                    blockObject.layer = 6;

                    var block = blockObject.AddComponent<Block>();
                    block.OnBlockPressed += PlayerMoveBlockInput;
                    block.OnFinishedMoving += OnBlockFinishedMoving;
                    block.Init(new Vector2Int(x, y), imageSlices[x, y]);
                    _blocks[x, y] = block;

                    if (y == 0 && x == blocksPerLine - 1)
                    {
                        _emptyBlock = block;
                    }
                }
            }

            camera.orthographicSize = blocksPerLine * .55f;
            _inputs = new Queue<Block>();
        }

        private void PlayerMoveBlockInput(Block blockToMove)
        {
            if (_state != PuzzleState.InPlay) return;
            _inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
        }

        private void MakeNextPlayerMove()
        {
            while (_inputs.Count > 0 && !_blockIsMoving)
            {
                MoveBlock(_inputs.Dequeue(), defaultMoveDuration);
            }
        }

        private void MoveBlock(Block blockToMove, float duration)
        {
            if ((blockToMove.coord - _emptyBlock.coord).sqrMagnitude == 1)
            {
                _blocks[blockToMove.coord.x, blockToMove.coord.y] = _emptyBlock;
                _blocks[_emptyBlock.coord.x, _emptyBlock.coord.y] = blockToMove;

                (_emptyBlock.coord, blockToMove.coord) = (blockToMove.coord, _emptyBlock.coord);

                var transform1 = _emptyBlock.transform;
                Vector2 targetPosition = transform1.position;
                transform1.position = blockToMove.transform.position;
                blockToMove.MoveToPosition(targetPosition, duration);
                _blockIsMoving = true;
            }
        }

        private void OnBlockFinishedMoving()
        {
            _blockIsMoving = false;
            CheckIfSolved();

            if (_state == PuzzleState.InPlay)
            {
                MakeNextPlayerMove();
            }
            else if (_state == PuzzleState.Shuffling)
            {
                if (_shuffleMovesRemaining > 0)
                {
                    MakeNextShuffleMove();
                }
                else
                {
                    _state = PuzzleState.InPlay;
                }
            }
        }

        private void StartShuffle()
        {
            _state = PuzzleState.Shuffling;
            _shuffleMovesRemaining = shuffleLength;
            _emptyBlock.gameObject.SetActive(false);
            MakeNextShuffleMove();
        }

        private void MakeNextShuffleMove()
        {
            Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
            var randomIndex = Random.Range(0, offsets.Length);

            for (int i = 0; i < offsets.Length; i++)
            {
                Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];
                if (offset != _prevShuffleOffset * -1)
                {
                    Vector2Int moveBlockCoord = _emptyBlock.coord + offset;

                    if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine)
                    {
                        MoveBlock(_blocks[moveBlockCoord.x, moveBlockCoord.y], shuffleMoveDuration);
                        _shuffleMovesRemaining--;
                        _prevShuffleOffset = offset;
                        break;
                    }
                }
            }
      
        }

        private void CheckIfSolved()
        {
            if (_blocks.Cast<Block>().Any(block => !block.IsAtStartingCoord()))
            {
                return;
            }

            _state = PuzzleState.Solved;
            _emptyBlock.gameObject.SetActive(true);
            successText.gameObject.SetActive(true);
            _solvedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}
