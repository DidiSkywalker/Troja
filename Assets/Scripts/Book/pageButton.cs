using System.Collections;
using System.Collections.Generic;
using Page;
using Events.Channels;
using UnityEngine;

public class pageButton : MonoBehaviour
{
    public PageSO pageToDisplay;
    public BookEventChannelSO eventChannel;

    public void OnButtonClick()
    {
        eventChannel.RaiseEvent(pageToDisplay);
    }
}
