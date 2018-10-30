using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

    // Allows us to use this script publically
    public static ObjectPoolManager current;
    // Object 
    public GameObject pooledObject;
    // The amount of objects you want in the pool
    public int pooledAmount;
    // pool growth bool
    public bool willGrow = true;
    // List of objects
    List<GameObject> pooledObjects;

     void Awake()
    {
        current = this;
    }

    // Use this for initialization
    void Start()
    {
        // Creating the list of objects
        pooledObjects = new List<GameObject>();
        // Cycles through the amount of objects thats in the pool
        for (int i = 0; i < pooledAmount; ++i)
        {
            // Gets the object and copies it
            GameObject obj = (GameObject)Instantiate(pooledObject);
            // Sets the objects in the pool to false (not being used)
            obj.SetActive(false);
            // Then adds that object into the list
            pooledObjects.Add(obj);
        }
    }

    // Getting the objects from the pool
    public GameObject GetPooledObject()
    {
        // Cycle through and count the objects in the pooled objects
        for(int i = 0; i < pooledObjects.Count; ++i)
        {
            // if the objects are inactive
            if(!pooledObjects[i].activeInHierarchy)
            {
                // retrun the objects
                return pooledObjects[i];
            }
        }

        // If will grow is true
        if(willGrow)
        {
            // Instiantiate a new object
            GameObject obj = (GameObject)Instantiate(pooledObject);
            // Then add the new object to the list
            pooledObjects.Add(obj);
            return obj;
        }

        // If the above code is not used return Null
        return null;
    }
}