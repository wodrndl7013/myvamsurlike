using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : Singleton<MonsterSpawner>
{
    public PooledObjectType objectType = PooledObjectType.Monster; // 풀에서 가져올 오브젝트 타입
    public float innerRadius = 20f;      // 스폰이 안되는 내부 반지름
    public float outerRadius = 30f;     // 스폰 가능한 외부 반지름
    public int spawnCount = 1;          // 초당 생성할 오브젝트 수
    public float spawnInterval = 1f;    // 오브젝트 생성 간격 (초당)
    public Transform movingObject;      // 기준이 되는 계속 움직이는 오브젝트

    [NonSerialized] public Dictionary<int, GameObject> MonsterInstance = new ();

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 spawnPosition = GetRandomPositionInDonut();

                // 풀에서 오브젝트를 가져와서 생성
                GameObject spawnedMonster = ObjectPoolManager.Instance.SpawnFromPool(objectType, spawnPosition, Quaternion.identity);

                if (spawnedMonster != null)
                {
                    AddMonsterDictionary(spawnedMonster);
                }
                else
                {
                    Debug.LogWarning("Failed to spawn object from pool.");
                }
            }

            // 다음 생성 주기까지 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 도넛 모양 범위 내에서 랜덤한 위치 반환
    private Vector3 GetRandomPositionInDonut()
    {
        Vector3 randomPosition;

        // 무작위 위치를 얻을 때까지 반복
        do
        {
            // 외부 반지름 안의 무작위 위치를 얻음
            Vector3 randomDirection = Random.insideUnitSphere * outerRadius;
            randomDirection.y = 0; // 2D 또는 평면에서의 생성이면 Y축을 0으로 고정
            randomPosition = movingObject.position + randomDirection;
        }
        // 무작위 위치가 내부 반지름 바깥쪽인 경우에만 위치 반환
        while (Vector3.Distance(movingObject.position, randomPosition) < innerRadius);

        return randomPosition;
    }

    private void AddMonsterDictionary(GameObject monster) // 딕셔너리에 생성된 몬스터 추가
    {
        MonsterInstance.Add(monster.GetInstanceID(), monster);
    }

    public List<GameObject> GetMonsterList() // 몬스터 값만 리스트로 반환
    {
        return MonsterInstance.Values.ToList();
    }

    public void RemoveMonster(int instanceID) // 죽은 몬스터를 딕셔너리에서 제거하고 풀로 반환
    {
        if (MonsterInstance.ContainsKey(instanceID))
        {
            GameObject monster = MonsterInstance[instanceID];
            ObjectPoolManager.Instance.ReturnToPool(PooledObjectType.Monster, monster); // 풀로 반환
            MonsterInstance.Remove(instanceID);
        }
    }
}