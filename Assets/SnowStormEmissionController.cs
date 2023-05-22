using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowStormEmissionController : MonoBehaviour
{ 
    public AnimationCurve emissionRateCurve;
    public float emissionSpeed = 1f; // Adjust this to control the speed of emission augmentation
    private ParticleSystem particleSystem;
    private float currentTime;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        currentTime = 0f;
    }

    private void Update()
    {
        currentTime += Time.deltaTime * emissionSpeed;
        float normalizedTime = currentTime / particleSystem.main.duration;
        //float emissionRate = emissionRateCurve.Evaluate(normalizedTime)*100;
        float emissionRate = currentTime += Time.deltaTime * emissionSpeed; 
        var emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = emissionRate;
    }
}
