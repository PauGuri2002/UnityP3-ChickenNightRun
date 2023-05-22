using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject switchOn, switchOff;
    public GameObject[] lightPanelON;
    public GameObject[] lightPanelOFF;
    public Light[] lightHelp;
    private bool toggleLight = true;

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
                toggleLight = false;
                switchOn.SetActive(true);
                switchOff.SetActive(false);
                foreach (Light light in lightHelp)
                {
                    light.enabled = true;
                }
                foreach (GameObject panel in lightPanelON)
                {
                    panel.SetActive(true);
                }
                foreach(GameObject panelOFF in lightPanelOFF)
                {
                    panelOFF.SetActive(false);
                }
            }
            else if (toggleLight == false)
            {
                toggleLight = true;
                switchOn.SetActive(false);
                switchOff.SetActive(true);
                foreach (Light light in lightHelp)
                {
                    light.enabled = false;
                }
                foreach (GameObject panel in lightPanelON)
                {
                    panel.SetActive(false);
                }
                foreach (GameObject panelOFF in lightPanelOFF)
                {
                    panelOFF.SetActive(true);
                }
            }
        }
    }
}
