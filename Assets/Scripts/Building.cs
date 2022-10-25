using System;
using Base;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int buildingGroupId;
    public Mesh ruinMesh;
    public Mesh builtMesh;

    private void Start()
    {
        if (CityState.Instance.IsBuildingGroupBuilt(buildingGroupId))
        {
            OnBuildingGroupBuilt(buildingGroupId);
        }
    }

    private void OnMouseOver()
    {
        if (State.IsMinigameRunning())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            CityState.Instance.OnBuildingGroupClicked(buildingGroupId);
        }
    }

    public void OnBuildingGroupBuilt(int groupId)
    {
        if (buildingGroupId == groupId)
        {
            GetComponent<MeshFilter>().mesh = builtMesh;
        }
    }
}