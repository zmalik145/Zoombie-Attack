using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler ShareInstance;
    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    // Start is called before the first frame update

    void Awake()
    {
        ShareInstance = this;  // it means there will be only one instance of static class ObjectPooler and it can be used by only this class
    }
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject fireBall = Instantiate(objectToPool);
            fireBall.SetActive(false);
            pooledObjects.Add(fireBall);
            fireBall.transform.SetParent(this.transform);
        }
    }

    public GameObject GetPooledObjects()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }

        }
        return null;
    }
}
