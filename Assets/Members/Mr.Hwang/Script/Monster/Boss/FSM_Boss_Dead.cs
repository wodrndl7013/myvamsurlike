using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mr.Hwang
{
    public class FSM_Boss_Dead : VMyState<FSM_BossState>
    {
        public override FSM_BossState StateEnum => FSM_BossState.FSM_BossState_Dead;
        private Boss _boss;

        void Awake()
        {
            base.Awake();
            _boss = GetComponent<Boss>();
        }

        protected override void EnterState()
        {
            _boss.Die();
        }

        protected override void ExcuteState()
        {

        }

        protected override void ExitState()
        {

        }
    }
}
