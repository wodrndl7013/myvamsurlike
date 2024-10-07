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

    // public List<Skill> SkillList;
    // public Skill currentSkill;
    // public Monster targetMonster;

    public List<BasicWeapon> BWList;  // 플레이어가 장착할 수 있는 무기 목록
    public BasicWeapon currentBW;    // 현재 장착된 무기
    public float BWCooltime; // 장착된 무기에 설정된 쿨타임 참조 변수
    public float currentCooltime; // 실제 쿨타임을 관장할 변수
    
    void Awake()
    {
        base.Awake();
        SettingBW();
    }

    private void FixedUpdate()
    {
        Vector3 nextVec = inputVec * (speed * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + nextVec);
    }

    // 인풋 시스템 함수
    void OnMove(InputValue value)
    {
       Vector2 moveInput = value.Get<Vector2>();
       inputVec = new Vector3(moveInput.x, 0, moveInput.y);
    }

    public void GetDamaged() // 데미지 받을 때
    {
        Debug.Log("데미지 받음");
    }

    private void SettingBW() // 인스펙터에서 설정된 BW 에 기반하여 쿨타임 설정
    {
        BWCooltime = currentBW.Info.Cooltime;
    }
    
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
