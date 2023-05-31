using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class volumeOptions : MonoBehaviour
{
    [Header("Settings")]
    public Volume volume;
    public VolumeProfile[] profiles;
    [HideInInspector] public VolumeProfile activeProfile;

    [Header("Drunk Settings")]
    private LensDistortion lensDistortion;
    private float previousIntensity;
    private float targetIntensity;
    public float transitionDuration = 1f;
    private float transitionTimer;
    private bool isIncreasing = true;
    private bool isFirstLoad = true;

    [Header("Disco Settings")]
    [SerializeField] private float redValor = 0.5f;
    [SerializeField] private float greenValor = 0.7f;
    [SerializeField] private float blueValor = 0.9f;
    [SerializeField] private float maxThreshold = 200f;
    [SerializeField] private float minThreshold = -200f;

    private void Start()
    {
        activeProfile = volume.profile;
        previousIntensity = 0f;
        targetIntensity = -1f;
        transitionTimer = 0f;
    }

    private void Update()
    {
        if (!string.Equals(activeProfile.name, volume.profile.name))
        {
            activeProfile = volume.profile;

            if (activeProfile.TryGet(out lensDistortion))
            {
                if (isFirstLoad)
                {
                    lensDistortion.intensity.value = targetIntensity;
                    previousIntensity = targetIntensity;
                    isFirstLoad = false;
                }
                else
                {
                    previousIntensity = lensDistortion.intensity.value;
                    if (isIncreasing)
                    {
                        targetIntensity = 1f;
                    }
                    else
                    {
                        targetIntensity = -1f;
                    }
                    transitionTimer = 0f;
                }
            }
        }

        if (lensDistortion != null && string.Equals(activeProfile.name, profiles[1].name))
        {
            if (transitionTimer < transitionDuration)
            {
                transitionTimer += Time.deltaTime;
                float t = transitionTimer / transitionDuration;
                float intensity = Mathf.Lerp(previousIntensity, targetIntensity, t);
                lensDistortion.intensity.value = intensity;
            }
            else
            {
                lensDistortion.intensity.value = targetIntensity;
                previousIntensity = targetIntensity;
                if (isIncreasing)
                {
                    targetIntensity = -1f;
                }
                else
                {
                    targetIntensity = 1f;
                }
                transitionTimer = 0f;
                isIncreasing = !isIncreasing;
            }
        }

        if (string.Equals(activeProfile.name, profiles[3].name))
        {
            ChannelMixer channelMixer;
            if (activeProfile.TryGet(out channelMixer))
            {
                float redLerpValue = Mathf.Lerp(minThreshold, maxThreshold, Mathf.PingPong(Time.time * redValor, transitionDuration) / transitionDuration);
                float greenLerpValue = Mathf.Lerp(minThreshold, maxThreshold, Mathf.PingPong(Time.time * greenValor, transitionDuration) / transitionDuration);
                float blueLerpValue = Mathf.Lerp(minThreshold, maxThreshold, Mathf.PingPong(Time.time * blueValor, transitionDuration) / transitionDuration);

                channelMixer.redOutRedIn.value = blueLerpValue;
                channelMixer.redOutGreenIn.value = redLerpValue;
                channelMixer.redOutBlueIn.value = greenLerpValue;

                channelMixer.greenOutRedIn.value = greenLerpValue;
                channelMixer.greenOutGreenIn.value = blueLerpValue;
                channelMixer.greenOutBlueIn.value = redLerpValue;

                channelMixer.blueOutRedIn.value = blueLerpValue;
                channelMixer.blueOutGreenIn.value = redLerpValue;
                channelMixer.blueOutBlueIn.value = greenLerpValue;
            }
        }
    }

    public void SetActiveProfile(int index)
    {
        activeProfile = profiles[index];
        Debug.Log(activeProfile.name);
    }
}






