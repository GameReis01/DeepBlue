using UnityEngine;

public class PortMarker : MonoBehaviour
{
    public string portName;
    public string description;
    public float dockingFee;
    public bool hasRefueling;
    public bool hasRepairFacilities;
    
    private void Start()
    {
        if (string.IsNullOrEmpty(description))
        {
            description = "A port along the Mediterranean coast.";
        }
        
        if (dockingFee <= 0)
        {
            dockingFee = Random.Range(10, 100);
        }
        
        hasRefueling = Random.value > 0.2f;
        hasRepairFacilities = Random.value > 0.4f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            Debug.Log("Ship entered port: " + portName);
            ShowPortInfo();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            Debug.Log("Ship left port: " + portName);
            HidePortInfo();
        }
    }
    
    private void ShowPortInfo()
    {
        Debug.Log("Showing port info for: " + portName);
        Debug.Log("- Description: " + description);
        Debug.Log("- Docking Fee: " + dockingFee + " credits");
        Debug.Log("- Refueling Available: " + (hasRefueling ? "Yes" : "No"));
        Debug.Log("- Repair Facilities: " + (hasRepairFacilities ? "Yes" : "No"));
    }
    
    private void HidePortInfo()
    {
        Debug.Log("Hiding port info for: " + portName);
    }
}
