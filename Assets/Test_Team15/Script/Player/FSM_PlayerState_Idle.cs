using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_PlayerState_Idle : VMyState<FSM_PlayerState>
{
    public override FSM_PlayerState StateEnum => FSM_PlayerState.FSM_Character1State_Idle;
    private Player _player;

    void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
    }

    protected override void EnterState()
    {
        
    }
    
    protected override void ExcuteState()
    {
        List<GameObject> monsters = MonsterSpawner.Instance.GetMonsterList();
        
        foreach (var skill in _player.SkillList)
        {
            if (skill.IsCooltiming()) // 쿨타임 중인 스킬 건너 뜀
                continue;
            
            foreach (var monsterObj in monsters)
            {
                Monster monster = monsterObj.GetComponent<Monster>();
                float distance = Vector3.Distance(transform.position, monster.transform.position);

                if (distance <= skill.SkillInfo.AttackDistance)
                {
                    _player.targetMonster = monster;
                    _player.currentSkill = skill;
                    _player.Fsm.ChangeState(FSM_PlayerState.FSM_Character1State_Skill); // 스킬 상태로 전환
                    return;
                }
            }
        }
    }
    
    protected override void ExitState()
    {
        
    }
}
