using Events.Channels;
using Enums;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public MovementEventChannelSO movementEventChannel;

    public void OnButtonClick()
    {
        movementEventChannel.RaiseMovementEvent(AnimatedMovements.SPEECHBUBBLESCALEDOWN);
        movementEventChannel.RaiseMovementEvent(AnimatedMovements.SLIDEOUT);
        print("exit button clicked");
    }
}

