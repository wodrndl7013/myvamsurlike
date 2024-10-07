using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FSM_PlayerState
{
    FSM_Character1State_Idle,
    FSM_Character1State_Skill
}

public class FSM_Player : StateMachine<FSM_PlayerState>
{
}
