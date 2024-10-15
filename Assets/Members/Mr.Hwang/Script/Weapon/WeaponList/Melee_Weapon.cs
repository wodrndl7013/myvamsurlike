using System;
using System.Collections;
using UnityEngine;

namespace Mr.Hwang
{
    public class Melee_Weapon : AbstractWeapon
    {
        [SerializeField] private float attackInterval = 2f; //추후 업글 시스템 연관지어야함
        private bool isAutoAttacking = false;

        private void Start()
        {
            StartAutoAttack();
        }

        public override void Attack()
        {
            // 공격 로직
            if (currentState == WeaponState.IDLE)
            {
                Debug.Log("근접 공격 하는지 확인용");
                //로직 추가하기
                ChangeState(WeaponState.ATTACKING);
            }
        }

        protected override void UpdateIdle()
        {
            // 대기 상태 업데이트 로직
        }

        protected void OnTriggerEnter(Collider other)
        {

        }

        protected override void UpdateAttacking()
        {
            // 공격 상태 업데이트 로직
            ChangeState(WeaponState.COOLDOWN);
        }

        protected override void UpdateCooldown()
        {
            // 쿨다운 상태 업데이트 로직
            ChangeState(WeaponState.IDLE);
        }

        public void SpecialAbilityA()
        {
            // 특수 능력 로직, 무기에 따라 추가
        }

        public void StartAutoAttack()
        {
            if (!isAutoAttacking)
            {
                isAutoAttacking = true;
                StartCoroutine(AutoAttackCoroutine());
            }
        }

        public void StopAutoAttack()
        {
            isAutoAttacking = false;
        }

        private IEnumerator AutoAttackCoroutine()
        {
            while (isAutoAttacking)
            {
                yield return new WaitForSeconds(attackInterval);
                Attack();
            }
        }
    }
}