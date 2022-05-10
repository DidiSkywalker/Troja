using System;
using Events.Channels;
using Minigames;
using TMPro;
using UnityEngine;

/// <summary>
/// Super simple minigame that prompts for a press of the spacebar.
/// Simply used as a placeholder and proof of concept.
/// </summary>
public class SpacebarMinigame : MonoBehaviour
{
    private long _successTime = -1;
    public long successDelay = 3;
    public TextMeshPro prompt;
    public TextMeshPro smallPrompt;
    public MinigameEventChannelSO minigameSuccessEventChannel;
    public MinigameSO minigame;

    private void Start()
    {
        print("Start Spacebar Minigame!");
    }

    private void Update()
    {
        var now = DateTimeOffset.Now.ToUnixTimeSeconds();

        if (_successTime > 0 && _successTime + successDelay >= now)
        {
            smallPrompt.text = $"Continue in {(_successTime + successDelay) - now}s";
        }
        
        if (_successTime < 0 && Input.GetKeyDown(KeyCode.Space))
        {
            _successTime = now;
            prompt.text = "Success!";
            prompt.color = Color.green;
        }
        else if (_successTime > 0 && _successTime + successDelay < now)
        {
            GameOver();
            _successTime = long.MaxValue;
        }
    }

    private void GameOver()
    {
        print("Spacebar Minigame Game Over");
        gameObject.SetActive(false);
        minigameSuccessEventChannel.RaiseEvent(minigame);
    }
}