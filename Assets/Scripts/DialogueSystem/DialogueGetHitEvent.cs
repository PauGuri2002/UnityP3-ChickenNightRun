using UnityEngine;

public class DialogueGetHitEvent : MonoBehaviour
{
    public void ExecuteEvent()
    {
        PlayerPhysics pp = FindObjectOfType<PlayerPhysics>();

        if (pp != null)
        {
            pp.GetHit(Vector3.up * 15 * -Physics.gravity.y, pp.transform.position);
        }
    }
}
