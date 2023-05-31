using UnityEngine;

public class SingleObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform targetPosition;
    private GameObject objectInstance;

    public void SpawnObject()
    {
        if (objectInstance == null)
        {
            objectInstance = Instantiate(prefab, targetPosition.position, Quaternion.identity);
            objectInstance.transform.SetParent(targetPosition);
        }
    }

    public void DespawnObject()
    {
        if (objectInstance != null)
        {
            Destroy(objectInstance);
            objectInstance = null;
        }
    }
}
