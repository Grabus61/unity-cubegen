using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    void Initialize()
    {
        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];

        Vector3[] directions = {
            Vector3.up,    // Top face
            Vector3.down,  // Bottom face
            Vector3.left,  // Left face
            Vector3.right, // Right face
            Vector3.forward,// Front face
            Vector3.back   // Back face
        };

        for (int i = 0; i < 6; i++)
        {
            if(meshFilters[i] == null)
            {
                GameObject face = new GameObject("Face" + i);
                face.transform.SetParent(transform);

                face.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                meshFilters[i] = face.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    void GenerateMesh()
    {
        foreach (var face in terrainFaces)
        {
            face.CreateMesh();
        }
    }
}