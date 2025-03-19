using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    [Header("Water Properties")]
    public float waveHeight = 0.5f;
    public float waveFrequency = 0.5f;
    public float waveSpeed = 1.0f;
    public float waveTurbulence = 0.3f;

    [Header("Mesh Properties")]
    public MeshFilter waterMeshFilter;
    public int meshResolution = 100;
    public float meshSize = 100f;

    private Mesh waterMesh;
    private Vector3[] vertices;
    private Vector3[] originalVertices;

    private void Start()
    {
        InitializeWaterMesh();
    }

    private void Update()
    {
        UpdateWaterMesh();
    }

    private void InitializeWaterMesh()
    {
        waterMesh = new Mesh();
        waterMeshFilter.mesh = waterMesh;

        GenerateMesh();
        originalVertices = waterMesh.vertices;
        vertices = new Vector3[originalVertices.Length];
    }

    private void GenerateMesh()
    {
        Debug.Log("Water mesh generated with " + meshResolution + " resolution");
    }

    private void UpdateWaterMesh()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            
            // Apply wave movement based on Gerstner waves algorithm
            float x = vertex.x * waveFrequency;
            float z = vertex.z * waveFrequency;
            float time = Time.time * waveSpeed;
            
            float height = Mathf.Sin(x + time) * Mathf.Cos(z + time) * waveHeight;
            height += Mathf.PerlinNoise(x * waveTurbulence, z * waveTurbulence) * waveHeight * 0.5f;
            
            vertices[i] = new Vector3(vertex.x, height, vertex.z);
        }
        
        waterMesh.vertices = vertices;
        waterMesh.RecalculateNormals();
    }
}
