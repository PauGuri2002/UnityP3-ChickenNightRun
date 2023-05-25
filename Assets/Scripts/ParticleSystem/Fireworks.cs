using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    public GameObject fireworks;
     public void SpawnFireworks()
    {       
            
            Instantiate(fireworks,transform.position,Quaternion.Euler(-90,0,0));
        
    }
}
