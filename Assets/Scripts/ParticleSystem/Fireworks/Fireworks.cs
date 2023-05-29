using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    public GameObject fireworks;
    private bool areSpawned = false;
     public void SpawnFireworks()
    {
        if (areSpawned) { return; }
        Instantiate(fireworks,transform.position,Quaternion.Euler(-90,0,0));
        areSpawned = true;
        
    }
}
