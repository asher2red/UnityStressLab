using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnRate = 100f;
    public int Count => objects.Count;
    private readonly List<GameObject> objects = new();
    private bool autoSpawn;

    public void Spawn()
    {
        Vector3 position = new Vector3(
            Random.Range(-5f, 5f),
            Random.Range(-3f, 3f),
            0f);
        
        var obj = Instantiate(prefab, position, Quaternion.identity);

        objects.Add(obj);
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
        foreach (var obj in objects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        objects.Clear();
    }

    public void setAutoSpawn(bool value)
    {
        autoSpawn = value;
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

    public void SetSpawnRate(float value)
    {
        spawnRate = value;
    }
}