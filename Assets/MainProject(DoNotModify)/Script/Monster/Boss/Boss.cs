using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : CharacterBase<FSM_Boss>, IDamageable
{
    public Rigidbody target;

    public float Speed = 3;
    public float Hp = 100;
    public float currentHp;

    [NonSerialized] public bool isDead = false;

    public List<BossSkill> BSList; // 보스 스킬 리스트
    public BossSkill currentBS; // 현재 발동할 스킬
    public float BSCooltime; // 현재 발동할 스킬의 쿨타임
    public float currentCooltime; // 실제 쿨타임의 변화를 측정할 변수

    void Awake()
    {
        base.Awake();
        FindPlayer();
        currentHp = Hp;
    }
    
    private void Start()
    {
        SettingBS();
    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            target = playerObject.GetComponent<Rigidbody>();

            if (target == null)
            {
                Debug.LogWarning("Player object does not have a Rigidbody.");
            }
        }
        else
        {
            Debug.LogWarning("No Player object found.");
        }
    }

    public void TrackingPlayer()
    {
        Vector3 dirVec = target.position - _rb.position;
        Vector3 nextVec = dirVec.normalized * (Speed * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + nextVec);
    }

    public void GetDamaged(float damage)
    {
        currentHp -= damage;
        Debug.Log($"남은 HP {currentHp}");
    }

    public void Die()
    {
        isDead = true;
    }
    
    private void SettingBS() // 인스펙터에서 설정된 BS 에 기반하여 쿨타임 설정
    {
        BSCooltime = currentBS.Info.Cooltime;
        currentBS.projectileType = currentBS.projectileData.objectTypeName;
    }

    public bool IsTargetInRange()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance < currentBS.Info.AttackDistance;
    }

    public bool IsCooltiming() // 쿨타임 중인지 확인
    {
        return currentCooltime > 0.0f;
    }

    public void StartCooltime() // 쿨타임 시작
    {
        StartCoroutine(StartCooltime_Internal());

        ObjectPoolManager.Instance.SpawnFromPool("Skill_A", transform.position, Quaternion.identity);
    }

    IEnumerator StartCooltime_Internal() // 쿨타임 코루틴
    {
        currentCooltime = BSCooltime;
        while (currentCooltime > 0.0f)
        {
            currentCooltime -= Time.deltaTime;
            yield return null;
        }
    }
}

