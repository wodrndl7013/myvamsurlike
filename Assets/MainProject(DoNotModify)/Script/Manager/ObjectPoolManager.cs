using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PooledObjectType
{
    None,
    Monster,
    ExpOrb,
    BW_Bullet,
    BW_Melee
}

[System.Serializable]
public class PooledObject
{
    public PooledObjectType type;
    public GameObject prefab;
    public int poolSize;
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public List<PooledObject> objectsToPool;

    private Dictionary<PooledObjectType, Queue<GameObject>> poolDictionary;
    
    void Awake()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<PooledObjectType, Queue<GameObject>>();

        foreach (PooledObject item in objectsToPool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(item.type, objectPool);
        }
    }
    
    public GameObject SpawnFromPool(PooledObjectType type, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning("Pool with type " + type + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[type].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        return objectToSpawn;
    }
    
    public void ReturnToPool(PooledObjectType type, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning("Pool with type " + type + " doesn't exist.");
            return;
        }

        IPooledObject pooledObj = objectToReturn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectReturn();
        }

        objectToReturn.SetActive(false);
        poolDictionary[type].Enqueue(objectToReturn);
    }
}

public interface IPooledObject
{
    void OnObjectSpawn();
    void OnObjectReturn();
}

// 사용예시
public class Bullet : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn()
    {
        // 총알이 스폰될 때 초기화 로직
    }

    public void OnObjectReturn()
    {
        // 총알이 풀로 반환될 때 정리 로직
    }

    private void Update()
    {
        // 총알 이동 로직
        // 만약 총알이 수명이 다했거나 충돌했다면:
        // ObjectPoolManager.Instance.ReturnToPool(PooledObjectType.Bullet, gameObject);
    }
}
