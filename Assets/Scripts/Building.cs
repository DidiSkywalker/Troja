using System;
using Events.Channels;
using Minigames;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Temporary script to attach to a building as a way to launch a minigame.
/// </summary>
public class Building : MonoBehaviour
{
    public MinigameSO minigame;
    public MinigameParams minigameParams;
    public MinigameEventChannelSO launchMinigameEventChannelSo;
    public Texture2D pointerCursor;

    private void OnMouseOver()
    {
        if (MinigameState.IsMinigameRunning())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && launchMinigameEventChannelSo != null)
        {
            print($"Building attempts to launch Minigame {minigame.minigameName}");
            launchMinigameEventChannelSo.RaiseEvent(minigame, minigameParams);
        }
    }

    private void OnMouseEnter()
    {
        // Cursor.SetCursor(pointerCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}