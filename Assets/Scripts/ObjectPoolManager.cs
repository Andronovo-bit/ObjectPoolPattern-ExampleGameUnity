using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    // A dictionary to store the pools of different types of objects
    private Dictionary<string, Queue<GameObject>> pools;

    // A singleton instance of this class
    public static ObjectPoolManager Instance { get; private set; }

    // Awake method to initialize the singleton instance and the pools dictionary
    private void Awake()
    {
        // Make sure there is only one instance of this class
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize the pools dictionary
        pools = new Dictionary<string, Queue<GameObject>>();
    }

    // A method to create a new pool for a given object type and size
    public void CreatePool(string objectType, GameObject prefab, int size)
    {
        // Check if the pool already exists
        if (pools.ContainsKey(objectType))
        {
            Debug.LogWarning("The pool for " + objectType + " already exists.");
            return;
        }

        // Create a new queue to store the objects
        Queue<GameObject> queue = new Queue<GameObject>();

        // Instantiate the objects and add them to the queue
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }

        // Add the queue to the pools dictionary with the object type as the key
        pools.Add(objectType, queue);
    }

    // A method to get an object from a pool by its type
    public GameObject GetObject(string objectType, GameObject prefab)
    {
        // Check if the pool exists
        if (!pools.ContainsKey(objectType))
        {
            Debug.LogError("The pool for " + objectType + " does not exist.");
            return null;
        }

        // Get the queue of the pool
        Queue<GameObject> queue = pools[objectType];

        // Check if the queue is empty added new object
        if (queue.Count == 0)
        {
            GameObject objAgain = Instantiate(prefab);
            objAgain.SetActive(false);
            queue.Enqueue(objAgain);
        }

        // Dequeue an object from the queue and return it
        GameObject obj = queue.Dequeue();
        return obj;
    }

    // A method to return an object to a pool by its type
    public void ReturnObject(string objectType, GameObject obj)
    {
        // Check if the pool exists
        if (!pools.ContainsKey(objectType))
        {
            Debug.LogError("The pool for " + objectType + " does not exist.");
            return;
        }

        // Get the queue of the pool
        Queue<GameObject> queue = pools[objectType];

        // Set the object to inactive and enqueue it to the queue
        obj.SetActive(false);
        queue.Enqueue(obj);
    }
}
