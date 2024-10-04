using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_MonsterState_Idle : VMyState<FSM_MonsterState>
{
    public override FSM_MonsterState StateEnum => FSM_MonsterState.FSM_MonsterState_Idle;
    private Monster _monster;

    void Awake()
    {
        base.Awake();
    }
    
    protected override void EnterState()
    {
        
    }
    
    protected override void ExcuteState()
    {
        
    }
    
    protected override void ExitState()
    {
        
    }
}
