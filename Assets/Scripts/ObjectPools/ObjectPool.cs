using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : Singleton<ObjectPool<T>> where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] private int InitialPoolSize;
    [SerializeField] private T Prefab;
    
    public int CurrentlyActive = 0;

    private Stack<T> pooledObjects;

    protected override void Awake()
    {
        base.Awake();
        pooledObjects = new Stack<T>(InitialPoolSize);
        Spawn(InitialPoolSize);
    }

    private void Spawn(int amount)
    {
        T instance;

        for (int i = 0; i < amount; i++)
        {
            instance = GameObject.Instantiate(Prefab);
            instance.gameObject.SetActive(false);
            pooledObjects.Push(instance);
        }
    }

    private void Release(T instance)
    {
        instance.gameObject.SetActive(false);
        pooledObjects.Push(instance);
        CurrentlyActive--;
    }

    public T Pull()
    {
        T instance;

        if (pooledObjects.Count > 0)
            instance = pooledObjects.Pop();
        else
            instance = GameObject.Instantiate(Prefab);

        instance.gameObject.SetActive(true);
        instance.InitPoolable(Release);

        CurrentlyActive++;

        return instance;
    }
}
