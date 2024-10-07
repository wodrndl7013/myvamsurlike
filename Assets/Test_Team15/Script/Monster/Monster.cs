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
        currentHp -= damage;
        Debug.Log($"남은 HP {currentHp}");
    }

    void Update()
    {
        if (!isDead && currentHp <= 0)
        {
            Fsm.ChangeState(FSM_MonsterState.FSM_MonsterState_Dead);
        }
    }
}
