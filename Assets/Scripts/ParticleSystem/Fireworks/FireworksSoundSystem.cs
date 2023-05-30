using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksSoundSystem : MonoBehaviour
{

    [SerializeField]
    private AudioClip BornSound;

    [SerializeField]
    private AudioClip DieSound;

    private ParticleSystem _parentParticleSystem;


    private int _currentNumberOfParticles = 0;

    AudioSource audioSource;

    void Start()
    {
        _parentParticleSystem = this.GetComponent<ParticleSystem>();
        if (_parentParticleSystem == null)
        {
            Debug.LogError("Missing ParticleSystem!", this);
        }

        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        var quantity = Mathf.Abs(_currentNumberOfParticles - _parentParticleSystem.particleCount);

        if (_parentParticleSystem.particleCount > _currentNumberOfParticles)
        {
            StartCoroutine(PlayMultipleSounds(quantity, BornSound));
        }

        if (_parentParticleSystem.particleCount < _currentNumberOfParticles)
        {
            StartCoroutine(PlayMultipleSounds(quantity, DieSound));
        }

        _currentNumberOfParticles = _parentParticleSystem.particleCount;
    }

    private IEnumerator PlayMultipleSounds(int quantity, AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
        yield return new WaitForSeconds(0.0f);

    }
}
