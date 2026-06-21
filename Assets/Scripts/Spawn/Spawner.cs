using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnMode mode;
    [SerializeField] private GameObject prefab;
    [SerializeField] private PoolableCube poolablePrefab;
    [SerializeField] private float spawnRate = 100f;
    [SerializeField] private float lifeTime = 3f;

    private ObjectPool<PoolableCube> pool;
    public int Count => objects.Count;
    public float LifeTime => lifeTime;
    private readonly List<GameObject> objects = new();
    private bool autoSpawn;

    private void Start()
    {
        pool = new ObjectPool<PoolableCube>(poolablePrefab, 1000, transform);
    }

    public void Spawn()
    {
        Vector3 position = new Vector3(
            Random.Range(-5f, 5f),
            Random.Range(-3f, 3f),
            0f);
        
        switch (mode)
        {
            case SpawnMode.Destroy:
                {
                    var obj = Instantiate(prefab, position, Quaternion.identity);

                    objects.Add(obj);

                    StartCoroutine(DespawnRoutine(obj));

                    break;
                }

            case SpawnMode.Pool:
                {
                    var obj = pool.Get();
                    obj.transform.position = position;

                    objects.Add(obj.gameObject);

                    StartCoroutine(DespawnRoutine(obj.gameObject));

                    break;
                }
        }

    }

    public void SpawnMany(int count)
    {
        for (int i = 0; i<count; i++)
        {
            Spawn();
        }
    }

    public void Clear()
    {
        StopAllCoroutines();
        
        foreach (var obj in objects)
        {
            if (obj == null)
                continue;

            if (mode == SpawnMode.Destroy)
            {
                Destroy(obj);
            }
            else
            {
                pool.Release(obj.GetComponent<PoolableCube>());
            }
        }

        objects.Clear();
    }

    private IEnumerator DespawnRoutine(GameObject obj)
    {
        yield return new WaitForSeconds(lifeTime);

        if (obj == null)
            yield break;

        if (mode == SpawnMode.Destroy)
        {
            objects.Remove(obj);

            Destroy(obj);
        }
        else
        {
            objects.Remove(obj);

            pool.Release(obj.GetComponent<PoolableCube>());
        }
    }

    private void Update()
    {
        if (!autoSpawn)
        return;

        float count = spawnRate * Time.deltaTime;

        for (int i = 0; i < count; i++)
        {
            Spawn();
        }
    }

    public void setAutoSpawn(bool value)
    {
        autoSpawn = value;
    }

    public void SetPoolMode(bool value)
    {
        if (value)
        {
            mode = SpawnMode.Pool;
        }
        else
        {
            mode = SpawnMode.Destroy;
        }
    }

    public void SetSpawnRate(float value)
    {
        spawnRate = value;
    }

    public void SetLifeTime(float value)
    {
        lifeTime = value;
    }
}