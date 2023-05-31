using UnityEngine;

public class DialogueGetHitEvent : MonoBehaviour
{
    public void ExecuteEvent()
    {
        PlayerPhysics pp = FindObjectOfType<PlayerPhysics>();

        if (pp != null)
        {
            pp.GetHit(150 * -Physics.gravity.y * Vector3.up, pp.transform.position);
        }
    }
}
