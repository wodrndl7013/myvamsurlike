using System;
using System.Collections;
using System.Collections.Generic;
using Mr.Hwang;
using UnityEngine;

namespace Mr.Hwang
{
    public class BossSkill_TimeBomb : BossSkill
    {
        private Transform timeBomb;
        public float time = 3f;

        private void Awake()
        {
            projectileType = PooledObjectType.BS_TimeBomb;
            timeBomb = transform.Find("timeBomb");
            
            if (timeBomb != null)
            {
                timeBomb.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            if (timeBomb != null)
            {
                timeBomb.gameObject.SetActive(false);
            }
            
            StartCoroutine(TimeBombTicking());
        }

        IEnumerator TimeBombTicking()
        {
            yield return new WaitForSeconds(time);

            TimeBombActivating();
        }

        void TimeBombActivating()
        {
            // 폭탄을 활성화
            if (timeBomb != null)
            {
                timeBomb.gameObject.SetActive(true);
                Debug.Log("Time Bomb Activated!");
            }

            StartCoroutine(ReturnToPool());
        }

        IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(1f);
            
            // 폭탄 비활성화 (풀로 돌아가기 전에)
            if (timeBomb != null)
            {
                timeBomb.gameObject.SetActive(false);
            }
            
            ObjectPoolManager.Instance.ReturnToPool(projectileType, gameObject);
        }
    }
}
