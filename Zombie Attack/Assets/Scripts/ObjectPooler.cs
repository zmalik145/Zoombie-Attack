using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler ShareInstance;
    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    
    void Awake()
    {
        ShareInstance = this;  // it means there will be only one instance of static class ObjectPooler and it can be used by only this class
    }
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject fireBall = Instantiate(objectToPool); //instantiate the object to pool
            fireBall.SetActive(false);                         //then deactivate the object
            pooledObjects.Add(fireBall);                        //add the object in pooled object list
            fireBall.transform.SetParent(this.transform);        //set its tranform to its parent
        }
    }

    public GameObject GetPooledObjects()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy) //check if pool is not activated in hierachy
            {
                return pooledObjects[i];            //return it
            }

        }
        return null;
    }
}
