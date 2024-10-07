using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SkillInfo
{
    public float AttackDistance;
    public float Cooltime;
    public float Damage;
    public float Speed;
}

public class Skill : MonoBehaviour
{
    public SkillInfo SkillInfo;  // Inspector에서 설정 가능한 SkillInfo
    public PooledObjectType objectType;
    
    private Vector3 moveDirection;  // 처음 설정된 이동 방향
    private Vector3 startPosition;  // 시작 위치를 저장

    private bool isMoving = false;  // 이동 중 여부 체크

    public float currentCooltime;

    private void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }
    
    public void Initialize(Vector3 targetPosition, Vector3 spawnPosition)
    {
        GameObject spawnSkill = ObjectPoolManager.Instance.SpawnFromPool(objectType, spawnPosition, Quaternion.identity);
        Skill spawnedSkill = spawnSkill.GetComponent<Skill>();

        if (spawnedSkill != null)
        {
            spawnedSkill.startPosition = spawnSkill.transform.position;
            spawnedSkill.moveDirection = (targetPosition - spawnPosition).normalized;
            spawnedSkill.isMoving = true;
            spawnedSkill.StartCooltime(); // 쿨타임 시작
        }
    }

    private void MoveTowardsTarget()
    {
        // 처음 설정된 방향으로 이동
        transform.position += moveDirection * (SkillInfo.Speed * Time.deltaTime);

        // 지정된 AttackDistance만큼 이동했으면 풀로 반환
        if (Vector3.Distance(startPosition, transform.position) >= SkillInfo.AttackDistance)
        {
            isMoving = false;
            ObjectPoolManager.Instance.ReturnToPool(objectType, gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamaged(SkillInfo.Damage);
        }
    }

    public bool IsCooltiming()
    {
        return currentCooltime > 0.0f;
    }

    public void StartCooltime()
    {
        StartCoroutine(StartCooltime_Internal());
    }

    IEnumerator StartCooltime_Internal()
    {
        currentCooltime = SkillInfo.Cooltime;
        while (currentCooltime > 0.0f)
        {
            currentCooltime -= Time.deltaTime;
            yield return null;
        }
    }
}