using UnityEngine;
public class PlayMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicClip;

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component assigned to PlayMusic script in " + gameObject.name);
            return;
        }

        audioSource.clip = musicClip;
        audioSource.Play();
    }
}








