using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_MonsterState_Dead : VMyState<FSM_MonsterState>
{
    public override FSM_MonsterState StateEnum => FSM_MonsterState.FSM_MonsterState_Dead;
    private Monster _monster;

    void Awake()
    {
        base.Awake();
    }
    
    protected override void EnterState()
    {
        MonsterSpawner.Instance.RemoveMonster(gameObject.GetInstanceID());
        ObjectPoolManager.Instance.ReturnToPool(MonsterSpawner.Instance.objectType, gameObject);
    }
    
    protected override void ExcuteState()
    {
        
    }
    
    protected override void ExitState()
    {
        
    }
}
