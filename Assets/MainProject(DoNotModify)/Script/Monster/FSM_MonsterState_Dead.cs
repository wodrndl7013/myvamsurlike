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
        _monster = GetComponent<Monster>();
    }
    
    protected override void EnterState()
    {
        _monster.Die(); // 몬스터가 죽으면 처리
    }
    
    protected override void ExcuteState()
    {
        _monster.Fsm.ChangeState(FSM_MonsterState.FSM_MonsterState_Idle);
    }
    
    protected override void ExitState()
    {
        MonsterSpawner.Instance.RemoveMonster(gameObject.GetInstanceID()); // !!! 오브젝트를 false or Destroy하는 함수 = 이 함수 전에 모든 기능이 구현되어야 함.
    }
}
