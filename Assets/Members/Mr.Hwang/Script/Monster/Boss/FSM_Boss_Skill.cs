using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mr.Hwang
{
    public class FSM_Boss_Skill : VMyState<FSM_BossState>
    {
        public override FSM_BossState StateEnum => FSM_BossState.FSM_BossState_Skill;
        private Boss _boss;

        void Awake()
        {
            base.Awake();
            _boss = GetComponent<Boss>();
        }

        protected override void EnterState()
        {
            GameObject spawnSkill = ObjectPoolManager.Instance.SpawnFromPool(_boss.currentBS.projectileType,
                _boss.target.transform.position, Quaternion.identity);
        }

        protected override void ExcuteState()
        {
            _boss.Fsm.ChangeState(FSM_BossState.FSM_BossState_Idle);
        }

        protected override void ExitState()
        {
            _boss.StartCooltime();
        }
    }
}