using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : CharacterBase<FSM_Player>
{
    // Movement 필드
    private Vector3 inputVec;
    public float speed;
    
    // BasicWeapon 필드
    public List<BasicWeapon> BWList;  // 플레이어가 장착할 수 있는 무기 목록
    public BasicWeapon currentBW;    // 현재 장착된 무기
    public float BWCooltime; // 장착된 무기에 설정된 쿨타임 참조 변수
    public float currentCooltime; // 실제 쿨타임을 관장할 변수
    
    // HP 관련 필드 추가
    public float maxHealth = 100f; // 최대 HP
    private float currentHealth;   // 현재 HP
    public PlayerHealthBar healthBar; // PlayerHealthBar 참조
    
    // !!!! 10.19 수정 !!!!
    void Awake()
    {
        base.Awake();
    }
    // !!!! 수정 종료 !!!!

    // !!!! 10.19 추가 !!!!
    private void Start()
    {
        SettingBW(); // BasicWeapon 의 이름 설정이 Awake 에서 일어나므로 플레이어가 받아올 때 그보다 늦게 하기 위해 Start 에서 진행
        currentHealth = maxHealth; // 초기 HP 설정
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth); // 체력바 최대값 설정
        }
    }
    
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // 체력을 최대 체력으로 제한
        Debug.Log($"Player 체력 회복: {currentHealth}/{maxHealth}");
    }
    // !!!! 추가 종료 !!!!

    private void FixedUpdate()
    {
        Vector3 nextVec = inputVec * (speed * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + nextVec);
        
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth); // 체력 회복 시 체력바 업데이트
        }
    }

    // 인풋 시스템 함수
    void OnMove(InputValue value)
    {
       Vector2 moveInput = value.Get<Vector2>();
       inputVec = new Vector3(moveInput.x, 0, moveInput.y);
    }

    public void TakeDamage(float damage) // 데미지 받을 때
    {
        Debug.Log("데미지 받음");
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"Player took {damage} damage. Current HP: {currentHealth}");
        
        // HP 바 업데이트
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die() // 플레이어가 죽었을 때
    {
        Debug.Log("Player died");
        GameManager.Instance.GameOver();
    }
    
    // !!!! 10.19 수정 !!!!
    private void SettingBW() // 인스펙터에서 설정된 BW 에 기반하여 쿨타임 설정
    {
        BWCooltime = currentBW.Info.Cooltime;
        currentBW.projectileType = currentBW.projectileData.objectTypeName; // 프리팹에서 설정이 잘못되어 있을 경우를 방지
    }
    // !!!! 수정 종료 !!!!
    
    public bool IsCooltiming() // 쿨타임 중인지 확인
    {
        return currentCooltime > 0.0f;
    }
    
    public void StartCooltime() // 쿨타임 시작
    {
        StartCoroutine(StartCooltime_Internal());
    }

    IEnumerator StartCooltime_Internal() // 쿨타임 코루틴
    {
        currentCooltime = BWCooltime;
        while (currentCooltime > 0.0f)
        {
            currentCooltime -= Time.deltaTime;
            yield return null;
        }
    }

    void Update()
    {
        
    }
}
