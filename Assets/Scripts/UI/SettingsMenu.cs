using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public void HideScreen()
    {
        gameObject.SetActive(false);
    }

    public void SetFullscreenMode(bool value)
    {
        Screen.fullScreen = value;
    }

    public void SetAudioVolume(float value)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(value) * 20);
    }
}
