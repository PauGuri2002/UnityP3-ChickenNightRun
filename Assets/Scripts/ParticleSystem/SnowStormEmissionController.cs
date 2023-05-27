using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowStormEmissionController : MonoBehaviour
{ 
    [SerializeField]
    public float emissionSpeed = 1f; // Adjust this to control the speed of emission augmentation

    private ParticleSystem particleSystem;

    [SerializeField]
    public float startingSize = 10f;

    private float minCurveValue;
    private float maxCurveValue;

    [SerializeField]
    private float MinStartSizeSpeed = 0.1f;
    [SerializeField]
    private float MaxStartSizeSpeed = 0.15f;
    [SerializeField]
    private float maxSnowSizeValue = 1.2f;

    [SerializeField]
    public float noiseFrequencySpeed = 0.1f;
    [SerializeField]
    public float noiseStrengthSpeed = 0.01f;

    private float intensity = 0f;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        updateIntensity();
        augmentEmmission();
        augmentStartSize();
        modifyNoise();

    }
    
    private void updateIntensity()
    {
        intensity += Time.deltaTime;
    }

    private void augmentEmmission()
    {
        float normalizedTime = intensity / particleSystem.main.duration;
        float emissionRate = intensity * emissionSpeed;
        var emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = emissionRate;
    }

    private void augmentStartSize()
    {
        if (maxCurveValue > maxSnowSizeValue)
        {
            return;
        }
        minCurveValue = intensity * MinStartSizeSpeed;
        maxCurveValue = intensity * MaxStartSizeSpeed;
        var mainModule = particleSystem.main;
        mainModule.startSize = new ParticleSystem.MinMaxCurve(minCurveValue, maxCurveValue);
    }

    private void modifyNoise()
    {
        var noiseModule = particleSystem.noise;
        noiseModule.frequency = intensity * noiseFrequencySpeed;
        noiseModule.strength = intensity * noiseStrengthSpeed;
    }
}
