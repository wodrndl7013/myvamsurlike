using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CharacterBase<FSM_Monster>
{
    public Rigidbody target;
    
    public float Speed = 3;
    public float Hp = 10;
    public float currentHp;
   
    [NonSerialized]public bool isDead = false;
    
    // 경험치 오브 프리팹 (인스펙터에서 연결)
    public GameObject experienceOrbPrefab;
    public int experienceAmount = 10; // 몬스터가 줄 경험치 양
    
    void Awake()
    {
        base.Awake();
        FindPlayer();
        currentHp = Hp;
    }
    
    private void OnEnable()
    {
        currentHp = Hp;
    }

    void FixedUpdate()
    {
        TrackingPlayer();
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

    void TrackingPlayer()
    {
        Vector3 dirVec = target.position - _rb.position;
        Vector3 nextVec = dirVec.normalized * (Speed * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + nextVec);
    }

    public void GetDamaged(float damage)
    {
        Hp -= damage;
        Debug.Log($"남은 HP {Hp}");
    }
    void Update()
    {
        if (!isDead && Hp < 0)
        {
            Fsm.ChangeState(FSM_MonsterState.FSM_MonsterState_Dead);
        }
    }
    
    public void Die()
    {
        isDead = true;
        // 경험치 오브 생성
        SpawnExperienceOrb();
    }
    
    void SpawnExperienceOrb()
    {
        // 경험치 오브를 몬스터의 현재 위치에 스폰
        if (experienceOrbPrefab != null)
        {
            GameObject experienceOrb = Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);
            ExperienceOrb orbScript = experienceOrb.GetComponent<ExperienceOrb>();
            if (orbScript != null)
            {
                // 경험치 오브에 경험치 양을 설정
                orbScript.experienceAmount = experienceAmount;
            }
        }
        else
        {
            Debug.LogWarning("Experience orb prefab not assigned.");
        }
    }
}
