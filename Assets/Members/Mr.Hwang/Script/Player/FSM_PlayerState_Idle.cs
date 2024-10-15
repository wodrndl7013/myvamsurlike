using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mr.Hwang
{
    public class FSM_PlayerState_Idle : VMyState<FSM_PlayerState>
    {
        public override FSM_PlayerState StateEnum => FSM_PlayerState.FSM_PlayerState_Idle;
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
            if (!_player.IsCooltiming())
            {
                _player.Fsm.ChangeState(FSM_PlayerState.FSM_PlayerState_Skill);
            }
        }

        protected override void ExitState()
        {

        }
    }
}
