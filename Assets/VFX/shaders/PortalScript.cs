using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material material;
    private float startingdistance;
    private void OnTriggerEnter(Collider other)
    {
         startingdistance = Vector3.Distance(new Vector3(transform.position.x, other.transform.position.y, transform.position.z), other.transform.position);
    }
    private void OnTriggerStay(Collider other)
    {

        float distance;
        if (other.CompareTag("Player"))
        {
             
            distance  = Vector3.Distance(new Vector3(transform.position.x,other.transform.position.y,transform.position.z), other.transform.position);
            Debug.Log(distance);
            material.SetFloat("_Dissolve",  (distance /startingdistance) );
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            material.SetFloat("_Dissolve", 1);
        }

    }
}
