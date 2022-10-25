using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using Events.Channels;
using Minigames;
using UnityEngine;

public class CityState : MonoBehaviour
{
    public static CityState Instance;
    
    public List<BuildingGroup> buildingGroups;
    public MinigameEventChannelSO loadMinigameEventChannel;
    
    private readonly Dictionary<int, bool> _buildingGroupState = new();

    private void Start()
    {
        foreach (var group in buildingGroups)
        {
            _buildingGroupState.Add(group.groupId, false);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsBuildingGroupBuilt(int groupId)
    {
        return _buildingGroupState.ContainsKey(groupId) && _buildingGroupState[groupId];
    }

    public void OnBuildingGroupClicked(int groupId)
    {
        var group = buildingGroups.ToList().Find(g => g.groupId == groupId);
        if (group.groupId < 0)
        {
            print($"No Building Group is defined for {groupId}");
            return;
        }

        if (group.minigameParams == null)
        {
            group.minigameParams = ScriptableObject.CreateInstance<MinigameParams>();
        }
        group.minigameParams.groupId = groupId;
        loadMinigameEventChannel.RaiseEvent(group.minigame, group.minigameParams);
    }

    public void OnMinigameSuccess(MinigameSO minigame)
    {
        var groupId = State.Instance.MinigameParams.groupId;
        _buildingGroupState[groupId] = true;
        print($"Setting group {groupId} to true: {_buildingGroupState[groupId]}");
    }
}

[Serializable]
public class BuildingGroup
{
    public int groupId = -1;
    public MinigameSO minigame;
    public MinigameParams minigameParams;
}