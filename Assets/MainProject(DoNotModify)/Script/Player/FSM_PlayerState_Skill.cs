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
        GameObject spawnWeapon = ObjectPoolManager.Instance.SpawnFromPool(_player.currentBW.projectileType, transform.position, Quaternion.identity);
        BasicWeapon bw = spawnWeapon.GetComponent<BasicWeapon>(); 
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.WorldToScreenPoint(transform.position).z));
        bw.SetPositionInfo(mouseWorldPos, transform.position);
    }
    
    protected override void ExcuteState()
    {
        _player.Fsm.ChangeState(FSM_PlayerState.FSM_Character1State_Idle);
    }
    
    protected override void ExitState()
    {
        _player.StartCooltime();
    }
}
