using UnityEngine;

public class ClearDontDestroy : MonoBehaviour
{
    void Start()
    {
        var dontDestroys = GameObject.FindGameObjectsWithTag("DontDestroy");
        foreach (var dontDestroy in dontDestroys)
        {
            Destroy(dontDestroy);
        }
    }
}
