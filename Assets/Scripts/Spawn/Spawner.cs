using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public void Spawn()
    {
        Vector3 position = new Vector3(Random.Range(-5f, 5f),
            Random.Range(-3f, 3f),
            0f);
        
        Instantiate(prefab, position, Quaternion.identity);
    }
}