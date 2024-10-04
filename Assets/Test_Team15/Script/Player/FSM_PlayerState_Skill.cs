using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_PlayerState_Skill : VMyState<FSM_PlayerState>
{
    public override FSM_PlayerState StateEnum => FSM_PlayerState.FSM_Character1State_Skill;
    private Player _player;

    void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
    }

    protected override void EnterState()
    {
        if (_player.targetMonster != null && _player.currentSkill != null)
        {
            Vector3 targetPostiion = _player.targetMonster.transform.position;
            Skill activeSkill = _player.currentSkill;
            activeSkill.Initialize(targetPostiion, _player.transform.position);
        }
    }
    
    protected override void ExcuteState()
    {
        _player.Fsm.ChangeState(FSM_PlayerState.FSM_Character1State_Idle);
    }
    
    protected override void ExitState()
    {
        if (_player.currentSkill != null)
        {
            _player.currentSkill.StartCooltime();
        }
    }
}
