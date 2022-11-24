using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Pool<T> where T : MonoBehaviour, IPoolable<T>
{
    private T poolableObjectPrefab;
    private Stack<T> pooledObjects = new Stack<T>();

    public Pool(T poolableObjectPrefab, int initialPoolSize)
    {
        this.poolableObjectPrefab = poolableObjectPrefab;
        Spawn(initialPoolSize);
    }

    private void Spawn(int amount)
    {
        T instance;

        for (int i = 0; i < amount; i++)
        {
            instance = GameObject.Instantiate(poolableObjectPrefab);
            Add(instance);
        }
    }

    private void Add(T instance)
    {
        instance.gameObject.SetActive(false);
        pooledObjects.Push(instance);
    }

    public T Get()
    {
        T instance;

        if (pooledObjects.Count > 0)
            instance = pooledObjects.Pop();
        else
            instance = GameObject.Instantiate(poolableObjectPrefab);

        instance.gameObject.SetActive(true);
        instance.Init(Add);

        return instance;
    }
}
