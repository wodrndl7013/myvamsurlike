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
        _monster.Die(); // 몬스터가 죽으면 처리
    }
    
    protected override void ExcuteState()
    {
        //_monster.Fsm.ChangeState(FSM_MonsterState.FSM_MonsterState_Idle);
    }
    
    protected override void ExitState()
    {
        //_monster.currentHp = _monster.Hp;
    }
}
