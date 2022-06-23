using Base;
using Events.Channels;
using Minigames;
using UnityEngine;

/// <summary>
/// Temporary script to attach to a building as a way to launch a minigame.
/// </summary>
public class Building : MonoBehaviour
{
    public MinigameSO minigame;
    public MinigameParams minigameParams;
    public MinigameEventChannelSO launchMinigameEventChannelSo;

    private void OnMouseOver()
    {
        if (State.IsMinigameRunning())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && launchMinigameEventChannelSo != null)
        {
            launchMinigameEventChannelSo.RaiseEvent(minigame, minigameParams);
        }
    }
}