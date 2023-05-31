using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class SwapPostProcessing : MonoBehaviour
{
    public volumeOptions _volumeOptions;
    public Volume volume;

    private void Update()
    {
       if(!string.Equals(_volumeOptions.activeProfile.name, _volumeOptions.profiles[2].name))
       {
            volume.enabled = true;
       }
       else
       {
            volume.enabled = false;
            volume.isGlobal = false;
       }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            volume.isGlobal = true;
        }
    }

    public void VolumeOFF()
    {
        GetComponent<Volume>().isGlobal = false;
    }
}
