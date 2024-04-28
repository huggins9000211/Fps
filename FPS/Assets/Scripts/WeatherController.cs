using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{

    [SerializeField] ParticleSystem snowParticleSystem;
    [SerializeField] float snowIntensity = 1.0f;
    [SerializeField] float emissionRate = 100.0f;
    [SerializeField] float visibilityRange = 50.0f;
    [SerializeField] float fogDensity = 0.05f;
    [SerializeField] Color fogColor = Color.gray;
    [SerializeField] float fogStartDistance = 20.0f;
    [SerializeField] float fogEndDistance = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (snowParticleSystem != null)
            snowParticleSystem.Play();


        float offset = Time.time * 0.1f;
        RenderSettings.fogStartDistance = fogStartDistance + offset;
        RenderSettings.fogEndDistance = fogEndDistance + offset;
        // Set initial weather conditions
        SetWeatherConditions();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void SetWeatherConditions()
    {
        // Adjust snow intensity
        var emission = snowParticleSystem.emission;
        emission.rateOverTime = snowIntensity * emissionRate;

        float playerDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float normalizedDistance = Mathf.Clamp01(playerDistance / visibilityRange);
        RenderSettings.fogDensity = fogDensity * normalizedDistance;
        RenderSettings.fogColor = fogColor;
    }

    public void SetSnowIntensity(float intensity)
    {
        snowIntensity = intensity;
        SetWeatherConditions();
    }

}
