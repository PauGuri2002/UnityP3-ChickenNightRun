using UnityEngine;

public class MovementTracker : MonoBehaviour
{
    public Transform targetObject;
    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        float deltaX = transform.position.x - previousPosition.x;
        targetObject.Translate(new Vector3(-deltaX, 0, 0));
        previousPosition = transform.position;
    }
}

