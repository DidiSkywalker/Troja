using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBehaviour : MonoBehaviour
{
    public float width, height, brushSize, brushStrength;
    public int resolution;

    private Vector3[] newVertices;
    private Vector2[] newUV;
    private int[] newTriangles;
    private Vector3 mousePos;
    private Transform transform;
    private int vertexCount;
    private int triangleCount;

    void Start()
    {
        mousePos = new Vector3();
        vertexCount = (resolution + 1) * (resolution + 1);
        triangleCount = resolution * resolution * 2;

        print(vertexCount);
        print(triangleCount);
        print(triangleCount * 3);

        newVertices = new Vector3[vertexCount];

        for (int i = 0; i < (resolution + 1); i++)
        {
            for (int j = 0; j < (resolution + 1); j++)
            {
                newVertices[i * (resolution + 1) + j] = new Vector3((width / resolution) * i, (height / resolution) * j, 0);
            }
        }

        newTriangles = new int[triangleCount * 3];

        int baseVertexIndex, baseTriIndex = 0;
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                baseVertexIndex = i * (resolution + 1) + j;
                
                print(baseTriIndex);
                newTriangles[baseTriIndex] = baseVertexIndex + resolution + 2;
                newTriangles[baseTriIndex + 1] = baseVertexIndex + 1;
                newTriangles[baseTriIndex + 2] = baseVertexIndex;

                newTriangles[baseTriIndex + 3] = baseVertexIndex;
                newTriangles[baseTriIndex + 4] = baseVertexIndex + resolution + 1;
                newTriangles[baseTriIndex + 5] = baseVertexIndex + resolution + 2;

                baseTriIndex += 6;
            }
        }

        Mesh mesh = new Mesh();
        
        mesh.vertices = newVertices;
        //mesh.uv = newUV;
        mesh.triangles = newTriangles;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        transform = GetComponent<Transform>();

        transform.Translate(new Vector3(width / -2, height / -2, 0));
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            print(mousePos);

            RaycastHit hit;
            Vector3 interceptionPoint;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                interceptionPoint = hit.point;
                float distance;
                for (int i = 0; i < vertexCount; i++)
                {
                    distance = Vector3.Distance(interceptionPoint, newVertices[i] + transform.localPosition);
                    print("distance from vertex " + i + " is " + distance);
                    if (distance < brushSize) newVertices[i].z -= brushStrength;
                }

                Mesh mesh = new Mesh();

                mesh.vertices = newVertices;
                //mesh.uv = newUV;
                mesh.triangles = newTriangles;
                GetComponent<MeshFilter>().mesh = mesh;
                GetComponent<MeshCollider>().sharedMesh = mesh;
            }
        }
    }
}
