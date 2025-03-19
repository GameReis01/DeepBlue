using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [System.Serializable]
    public class WeatherState
    {
        public string name;
        public float wavesMultiplier;
        public float windStrength;
        public Color skyColor;
        public Color waterColor;
        public float fogDensity;
        public float rainIntensity;
        public float lightningFrequency;
    }
    
    [Header("Weather Properties")]
    public WeatherState[] weatherStates;
    public float weatherChangeSpeed = 0.1f;
    
    [Header("Components")]
    public Light directionalLight;
    public Material waterMaterial;
    public ParticleSystem rainSystem;
    public AudioSource windAudio;
    public AudioSource thunderAudio;
    
    private int currentWeatherIndex = 0;
    private int targetWeatherIndex = 0;
    private float transitionProgress = 1f;
    private WaterSystem waterSystem;
    private float lightningTimer;
    
    private void Start()
    {
        waterSystem = FindObjectOfType<WaterSystem>();
        InitializeWeather();
    }
    
    private void Update()
    {
        UpdateWeatherTransition();
        UpdateWeatherEffects();
    }
    
    private void InitializeWeather()
    {
        if (weatherStates.Length == 0)
        {
            Debug.LogError("No weather states defined!");
            return;
        }
        
        currentWeatherIndex = Random.Range(0, weatherStates.Length);
        targetWeatherIndex = currentWeatherIndex;
        ApplyWeatherState(weatherStates[currentWeatherIndex]);
    }
    
    public void ChangeWeather(int index)
    {
        if (index >= 0 && index < weatherStates.Length)
        {
            targetWeatherIndex = index;
            transitionProgress = 0f;
        }
    }
    
    public void ChangeWeatherRandom()
    {
        int newIndex = Random.Range(0, weatherStates.Length);
        while (newIndex == targetWeatherIndex && weatherStates.Length > 1)
        {
            newIndex = Random.Range(0, weatherStates.Length);
        }
        
        ChangeWeather(newIndex);
    }
    
    private void UpdateWeatherTransition()
    {
        if (transitionProgress < 1f)
        {
            transitionProgress += weatherChangeSpeed * Time.deltaTime;
            if (transitionProgress >= 1f)
            {
                transitionProgress = 1f;
                currentWeatherIndex = targetWeatherIndex;
            }
            
            WeatherState currentState = weatherStates[currentWeatherIndex];
            WeatherState targetState = weatherStates[targetWeatherIndex];
            
            WeatherState lerpedState = new WeatherState();
            lerpedState.wavesMultiplier = Mathf.Lerp(currentState.wavesMultiplier, targetState.wavesMultiplier, transitionProgress);
            lerpedState.windStrength = Mathf.Lerp(currentState.windStrength, targetState.windStrength, transitionProgress);
            lerpedState.skyColor = Color.Lerp(currentState.skyColor, targetState.skyColor, transitionProgress);
            lerpedState.waterColor = Color.Lerp(currentState.waterColor, targetState.waterColor, transitionProgress);
            lerpedState.fogDensity = Mathf.Lerp(currentState.fogDensity, targetState.fogDensity, transitionProgress);
            lerpedState.rainIntensity = Mathf.Lerp(currentState.rainIntensity, targetState.rainIntensity, transitionProgress);
            lerpedState.lightningFrequency = Mathf.Lerp(currentState.lightningFrequency, targetState.lightningFrequency, transitionProgress);
            
            ApplyWeatherState(lerpedState);
        }
    }
    
    private void ApplyWeatherState(WeatherState state)
    {
        if (waterSystem != null)
        {
            waterSystem.waveHeight = state.wavesMultiplier * waterSystem.waveHeight;
        }
        
        RenderSettings.fogColor = state.skyColor;
        RenderSettings.fogDensity = state.fogDensity;
        
        if (directionalLight != null)
        {
            directionalLight.color = state.skyColor;
            directionalLight.intensity = Mathf.Lerp(0.5f, 1f, 1f - state.fogDensity * 2f);
        }
        
        if (waterMaterial != null)
        {
            waterMaterial.SetColor("_WaterColor", state.waterColor);
        }
        
        if (rainSystem != null)
        {
            var emission = rainSystem.emission;
            emission.rateOverTime = state.rainIntensity * 1000f;
        }

        if (windAudio != null)
        {
            windAudio.volume = state.windStrength * 0.5f;
            windAudio.pitch = state.windStrength * 0.5f + 0.5f;
        }
    }
    
    private void UpdateWeatherEffects()
    {
        WeatherState currentState = weatherStates[targetWeatherIndex];
        
        if (currentState.lightningFrequency > 0)
        {
            lightningTimer -= Time.deltaTime;
            if (lightningTimer <= 0)
            {
                lightningTimer = Random.Range(1f, 10f) / currentState.lightningFrequency;
                CreateLightningEffect();
            }
        }
    }
    
    private void CreateLightningEffect()
    {
        if (directionalLight != null)
        {
            StartCoroutine(LightningFlash());
        }
        
        if (thunderAudio != null && Random.value < 0.7f)
        {
            StartCoroutine(PlayDelayedThunder());
        }
    }
    
    private System.Collections.IEnumerator LightningFlash()
    {
        float originalIntensity = directionalLight.intensity;
        Color originalColor = directionalLight.color;
        
        directionalLight.intensity = 2f;
        directionalLight.color = Color.white;
        
        yield return new WaitForSeconds(0.1f);
        
        directionalLight.intensity = originalIntensity;
        directionalLight.color = originalColor;
    }
    
    private System.Collections.IEnumerator PlayDelayedThunder()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        thunderAudio.pitch = Random.Range(0.8f, 1.2f);
        thunderAudio.Play();
    }
}
