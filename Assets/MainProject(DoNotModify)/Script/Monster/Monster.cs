using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CharacterBase<FSM_Monster>, IMonsterType
{
    public MonsterType monsterType; // 인스펙터에서 타입 지정
    public MonsterType MonsterType => monsterType; // 인스펙터에서 지정한 타입을 가져와서 해당 오브젝트의 타입으로 설정, FSM 전역에서 사용할때는 해당 변수를 사용
    
    public Rigidbody target;
    
    public float Speed = 3;
    public float Hp = 10;
    public float currentHp;
   
    [NonSerialized]public bool isDead = false;

    void Awake()
    {
        base.Awake();
        FindPlayer();
        currentHp = Hp;
    }
    
    private void OnEnable()
    {
        currentHp = Hp;
        isDead = false;
        Fsm.ChangeState(FSM_MonsterState.FSM_MonsterState_Idle);
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
        // 타입 설정 예시
        // if (MonsterType == MonsterType.Normal)
        // {
        //     // 일반 몬스터 사망시 로직 처리
        // }
        // else if (MonsterType == MonsterType.Elite)
        // {
        //     // 엘리트 몬스터 사망시 로직 처리
        // }

        isDead = true;
        
        if (monsterType == MonsterType.Normal)
        {
            RewardManager.Instance.DropExperienceOrb(transform.position);
        }
        else if (monsterType == MonsterType.Elite)
        {
            RewardManager.Instance.DropAbilityItem(transform.position);
        }
        
        // 확률에 따라 랜덤 아이템 드랍
        RewardManager.Instance.DropRandomItem(transform.position);
    }
}
