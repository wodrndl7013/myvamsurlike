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

    public List<Skill> SkillList;
    public Skill currentSkill;
    public Monster targetMonster;
    
    void Awake()
    {
        base.Awake();
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

    public void GetDamaged()
    {
        Debug.Log("데미지 받음");
    }

    void Update()
    {
        
    }
}
