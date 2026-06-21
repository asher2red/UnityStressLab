using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component, IPoolable
{
    private readonly Queue<T> pool = new();

    private readonly T prefab;
    private readonly Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i<initialSize; i++)
        {
            Create();
        }
    }

    private T Create()
    {
        T obj = Object.Instantiate(prefab, parent);
        obj.OnDespawn();
        pool.Enqueue(obj);

        return obj;
    }

    public T Get()
    {
        if (pool.Count == 0)
        {
            Create();
        }

        T obj = pool.Dequeue();
        obj.OnSpawn();

        return obj;
    }

    public void Release(T obj)
    {
        obj.OnDespawn();

        pool.Enqueue(obj);
    }
}