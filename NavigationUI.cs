using UnityEngine;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Text speedText;
    public Text coordinatesText;
    public Text weatherText;
    public Text depthText;
    public Text headingText;
    public RawImage miniMapImage;
    public RectTransform shipMarker;
    
    [Header("References")]
    public ShipController shipController;
    public WeatherSystem weatherSystem;
    
    private void Update()
    {
        if (shipController != null)
        {
            UpdateSpeedDisplay();
            UpdatePositionDisplay();
            UpdateHeadingDisplay();
        }
        
        if (weatherSystem != null)
        {
            UpdateWeatherDisplay();
        }
        
        UpdateDepthDisplay();
        UpdateMiniMap();
    }
    
    private void UpdateSpeedDisplay()
    {
        if (speedText != null)
        {
            float speed = shipController.GetComponent<Rigidbody>().velocity.magnitude;
            speedText.text = "Speed: " + speed.ToString("F1") + " knots";
        }
    }
    
    private void UpdatePositionDisplay()
    {
        if (coordinatesText != null)
        {
            Vector3 position = shipController.transform.position;

            coordinatesText.text = string.Format("Pos: {0:F1}, {1:F1}", position.x, position.z);
        }
    }
    
    private void UpdateHeadingDisplay()
    {
        if (headingText != null)
        {
            float heading = shipController.transform.eulerAngles.y;
            headingText.text = "Heading: " + heading.ToString("F0") + "°";
        }
    }
    
    private void UpdateWeatherDisplay()
    {
        if (weatherText != null)
        {

            weatherText.text = "Weather: Fair";
        }
    }
    
    private void UpdateDepthDisplay()
    {
        if (depthText != null)
        {

            float depth = 50f; // Placeholder value
            depthText.text = "Depth: " + depth.ToString("F1") + "m";
        }
    }
    
    private void UpdateMiniMap()
    {
        if (miniMapImage != null && shipMarker != null)
        {
            if (shipController != null)
            {

                Vector3 shipPosition = shipController.transform.position;

                float mapSize = 100f; // Placeholder value
                float x = (shipPosition.x / mapSize + 0.5f) * miniMapImage.rectTransform.rect.width;
                float y = (shipPosition.z / mapSize + 0.5f) * miniMapImage.rectTransform.rect.height;
                
                shipMarker.anchoredPosition = new Vector2(x, y);

                float rotation = -shipController.transform.eulerAngles.y;
                shipMarker.localRotation = Quaternion.Euler(0, 0, rotation);
            }
        }
    }
}
