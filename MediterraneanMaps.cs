using UnityEngine;

public class MediterraneanMaps : MonoBehaviour
{
    [Header("Map Properties")]
    public Transform mapParent;
    public float mapScale = 1f;
    
    [Header("Coastline Data")]
    public TextAsset coastlineData;
    public Material coastlineMaterial;
    
    [Header("Points of Interest")]
    public Transform[] turkishPorts;
    public Transform[] kktcPorts;
    public Transform[] specialLocations;
    
    private void Start()
    {
        if (coastlineData != null)
        {
            GenerateCoastlines();
        }
        
        PlacePortMarkers();
    }
    
    private void GenerateCoastlines()
    {
        Debug.Log("Generating Mediterranean coastlines from data...");

        GameObject coastline = new GameObject("Mediterranean Coastline");
        coastline.transform.SetParent(mapParent);
        
        LineRenderer lineRenderer = coastline.AddComponent<LineRenderer>();
        lineRenderer.material = coastlineMaterial;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        Vector3[] points = new Vector3[10];
        for (int i = 0; i < points.Length; i++)
        {
            float angle = i * Mathf.PI * 2f / points.Length;
            points[i] = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 50f;
        }
        
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
    
    private void PlacePortMarkers()
    {
        Debug.Log("Placing port markers for Turkish and KKTC ports...");

        CreatePortMarker("Adana Port", new Vector3(20, 0, 15));
        CreatePortMarker("Girne Port", new Vector3(-10, 0, 25));
        CreatePortMarker("Magosa Port", new Vector3(-5, 0, 20));
    }
    
    private void CreatePortMarker(string name, Vector3 position)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        marker.name = name;
        marker.transform.SetParent(mapParent);
        marker.transform.localPosition = position;
        marker.transform.localScale = new Vector3(1, 0.2f, 1);

        PortMarker portMarker = marker.AddComponent<PortMarker>();
        portMarker.portName = name;
    }
}
