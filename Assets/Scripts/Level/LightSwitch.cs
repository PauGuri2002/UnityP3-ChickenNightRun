using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject switchOn, switchOff;
    public GameObject[] lightPanel;
    public Light[] lightHelp;
    public bool toggleLight = true;

    private void Update()
    {
        Debug.Log("State:" + toggleLight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other);
            if (toggleLight == true)
            {
                //toggleLight = false;
                switchOn.SetActive(true);
                switchOff.SetActive(false);
                foreach (Light light in lightHelp)
                {
                    light.enabled = true;
                }
                foreach (GameObject panel in lightPanel)
                {
                    panel.SetActive(true);
                }
            }
            if (toggleLight == false)
            {
                //toggleLight=true;
                switchOn.SetActive(false);
                switchOff.SetActive(true);
                foreach (Light light in lightHelp)
                {
                    light.enabled = false;
                }
                foreach (GameObject panel in lightPanel)
                {
                    panel.SetActive(false);
                }
            }
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (toggleLight == true) 
    //        {
    //            Debug.Log("entra");
    //            toggleLight = false;
    //        }

    //        if (toggleLight == false)
    //        { 
    //            toggleLight = true; 
    //        }
    //    }
    //}
}
