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
    
    // 경험치 오브 프리팹 (인스펙터에서 연결)
    public TimerManager timerManager; // TimerManager 레퍼런스, 인스펙터에서 설정 필요
    public GameObject experienceOrbPrefab_Basic; // 기본 오브 프리팹
    public GameObject experienceOrbPrefab_Advanced; // 고급 오브 프리팹
    public GameObject experienceOrbPrefab_Premium; // 최고급 오브 프리팹
    
    void Awake()
    {
        base.Awake();
        FindPlayer();
        currentHp = Hp;
        if (timerManager == null)
        {
            timerManager = FindObjectOfType<TimerManager>();
            if (timerManager == null)
            {
                Debug.LogWarning("TimerManager가 씬에 존재하지 않습니다. TimerManager를 추가해주세요.");
            }
        }
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
        // 경험치 오브 생성
        SpawnExperienceOrb();
    }
    
    void SpawnExperienceOrb()
    {
        float currentTime = Time.timeSinceLevelLoad; // 씬이 로드된 후 경과된 시간을 사용
        Debug.Log($"Monster - Current Time Retrieved: {currentTime}"); // 디버그 로그 추가+
        
        string orbType = "ExperienceOrb_Basic";

        if (currentTime >= 10 && currentTime < 20) // 10분 ~ 20분 사이
        {
            float randomValue = UnityEngine.Random.value; // 0.0f ~ 1.0f 사이의 난수 생성
            if (randomValue <= 0.3f)
            {
                orbType = "ExperienceOrb_Advanced"; // 30% 확률로 고급 오브 드랍
            }
        }
        else if (currentTime >= 20) // 20분 이후
        {
            float randomValue = UnityEngine.Random.value; // 0.0f ~ 1.0f 사이의 난수 생성
            if (randomValue <= 0.1f)
            {
                orbType = "ExperienceOrb_Premium"; // 10% 확률로 최고급 오브 드랍
            }
            else if (randomValue <= 0.4f)
            {
                orbType = "ExperienceOrb_Advanced"; // 30% 확률로 고급 오브 드랍
            }
        }

        // 오브를 ObjectPoolManager에서 가져오기
        GameObject experienceOrb = ObjectPoolManager.Instance.SpawnFromPool(orbType, transform.position, Quaternion.identity);

        if (experienceOrb != null)
        {
            ExperienceOrb orbScript = experienceOrb.GetComponent<ExperienceOrb>();
            if (orbScript != null)
            {
                    orbScript.experienceAmount = orbType == "ExperienceOrb_Basic" ? 10 :
                        orbType == "ExperienceOrb_Advanced" ? 20 : 30;
                    orbScript.orbType = orbType; // 오브 타입 설정
            }
        }
        else
        {
            Debug.LogWarning($"{orbType} 생성에 실패했습니다.");
        }
    }

}
