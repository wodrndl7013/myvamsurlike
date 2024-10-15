using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mr.Hwang
{
    public enum FSM_BossState
    {
        FSM_BossState_Idle,
        FSM_BossState_Skill,
        FSM_BossState_Dead
    }

    public class FSM_Boss : StateMachine<FSM_BossState>
    {
    }
}
