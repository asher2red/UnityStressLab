using UnityEngine;

public class PoolableCube : MonoBehaviour, IPoolable
{
    public void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}