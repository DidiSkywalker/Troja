using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentPlacement : MonoBehaviour
{
    public Vector3 posOffset;
	public float sandPlaneColliderOffset;
	public GameObject parentObject;
	public GameObject sandPlane;
    public GameObject[] fragments;
    public GameObject[] rubble;
    public bool currentIsCorrectPosRot;
    public Vector3[] correctPos;
	public Vector3[] correctRot;

	public float[] fragmentVolumes;

	public int poissonSamples = 30;
	public float poissonRadius, poissonRadiusDecrement, poissonWidth, poissonHeight;

	private bool[] startOffIntersect;

	private float scale;
	private MeshCollider[] fragmentColliders;
	private MeshCollider sandPlaneCollider;

	void Start()
    {
		Transform emptyTransform = GetComponent<Transform>();
		scale = emptyTransform.localScale.x;

		startOffIntersect = new bool[fragments.Length];

		for (int i = 0; i < fragments.Length; i++)
        {
			startOffIntersect[i] = false;

		}

        if (currentIsCorrectPosRot)
        {
            correctPos = new Vector3[fragments.Length];
			correctRot = new Vector3[fragments.Length];
			fragmentVolumes = new float[fragments.Length];
			Transform fragmentTransform;
			//MeshFilter fragmentMesh;
            for (int i = 0; i < fragments.Length; i++)
            {
                fragmentTransform = fragments[i].GetComponent<Transform>();
				//fragmentMesh = fragments[i].GetComponent<MeshFilter>();
				correctPos[i] = fragmentTransform.localPosition;
				correctRot[i] = fragmentTransform.localEulerAngles;
				//fragmentVolumes[i] = VolumeOfMesh(fragmentMesh.mesh);
            }
		}

		fragmentColliders = new MeshCollider[fragments.Length];
		for (int i = 0; i < fragments.Length; i++)
		{
			fragmentColliders[i] = fragments[i].GetComponent<MeshCollider>();
		}

		sandPlaneCollider = sandPlane.GetComponent<MeshCollider>();

			List<Vector2> poissonPoints;
        do
        {
			poissonPoints = GeneratePoints(poissonRadius, new Vector2((poissonWidth / scale), (poissonHeight / scale)), poissonSamples);
			print("generated " + poissonPoints.Count + " points with radius " + poissonRadius);
			poissonRadius -= poissonRadiusDecrement;
        } while (poissonPoints.Count < fragments.Length);


		for (int i = 0; i < fragments.Length; i++)
		{
			Transform fragmentTransform = fragments[i].GetComponent<Transform>();
			fragmentTransform.localPosition = new Vector3(poissonPoints[i].x + posOffset.x - (poissonWidth / (scale * 2)), poissonPoints[i].y + posOffset.y - (poissonHeight / (scale * 2)), posOffset.z);
			fragmentTransform.localEulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }
    }

	public void updateFragmentDiscovery()
    {
		Vector3 direction;
		float distance;
		bool intersected;

        for (int i = 0; i < fragments.Length; i++)
        {
			intersected = Physics.ComputePenetration(
						fragmentColliders[i],
						fragmentColliders[i].gameObject.transform.position,
						fragmentColliders[i].gameObject.transform.rotation,
						sandPlaneCollider,
						sandPlaneCollider.gameObject.transform.position + new Vector3(0, 0, sandPlaneColliderOffset),
						sandPlaneCollider.gameObject.transform.rotation,
						out direction,
						out distance);

			if (intersected && !startOffIntersect[i])
            {
				startOffIntersect[i] = true;

			}

			if (!intersected && startOffIntersect[i])
            {
				fragments[i].GetComponent<Transform>().localPosition = correctPos[i];
				fragments[i].GetComponent<Transform>().localEulerAngles = correctRot[i];
				print(i);
			}
        }
		
	}

	private List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int numSamplesBeforeRejection)
	{
		float cellSize = radius / Mathf.Sqrt(2);

		int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)];
		List<Vector2> points = new List<Vector2>();
		List<Vector2> spawnPoints = new List<Vector2>();

		spawnPoints.Add(sampleRegionSize / 2);
		while (spawnPoints.Count > 0)
		{
			int spawnIndex = Random.Range(0, spawnPoints.Count);
			Vector2 spawnCentre = spawnPoints[spawnIndex];
			bool candidateAccepted = false;

			for (int i = 0; i < numSamplesBeforeRejection; i++)
			{
				float angle = Random.value * Mathf.PI * 2;
				Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
				Vector2 candidate = spawnCentre + dir * Random.Range(radius, 2 * radius);
				if (IsValid(candidate, sampleRegionSize, cellSize, radius, points, grid))
				{
					points.Add(candidate);
					spawnPoints.Add(candidate);
					grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
					candidateAccepted = true;
					break;
				}
			}
			if (!candidateAccepted)
			{
				spawnPoints.RemoveAt(spawnIndex);
			}

		}

		return points;
	}

	private bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid)
	{
		if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y)
		{
			int cellX = (int)(candidate.x / cellSize);
			int cellY = (int)(candidate.y / cellSize);
			int searchStartX = Mathf.Max(0, cellX - 2);
			int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
			int searchStartY = Mathf.Max(0, cellY - 2);
			int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

			for (int x = searchStartX; x <= searchEndX; x++)
			{
				for (int y = searchStartY; y <= searchEndY; y++)
				{
					int pointIndex = grid[x, y] - 1;
					if (pointIndex != -1)
					{
						float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
						if (sqrDst < radius * radius)
						{
							return false;
						}
					}
				}
			}
			return true;
		}
		return false;
	}

	public float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float v321 = p3.x * p2.y * p1.z;
		float v231 = p2.x * p3.y * p1.z;
		float v312 = p3.x * p1.y * p2.z;
		float v132 = p1.x * p3.y * p2.z;
		float v213 = p2.x * p1.y * p3.z;
		float v123 = p1.x * p2.y * p3.z;

		return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
	}

	public float VolumeOfMesh(Mesh mesh)
	{
		float volume = 0;

		Vector3[] vertices = mesh.vertices;
		int[] triangles = mesh.triangles;

		for (int i = 0; i < triangles.Length; i += 3)
		{
			Vector3 p1 = vertices[triangles[i + 0]];
			Vector3 p2 = vertices[triangles[i + 1]];
			Vector3 p3 = vertices[triangles[i + 2]];
			volume += SignedVolumeOfTriangle(p1, p2, p3);
		}
		return Mathf.Abs(volume);
	}
}


