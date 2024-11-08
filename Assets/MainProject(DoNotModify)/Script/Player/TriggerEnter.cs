using System.Collections;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    private Player _player;
    public float damageAmount = 5f; // 몬스터와 충돌 시 줄 데미지
    public float damageInterval = 0.5f; // 데미지 간격 (초)

    private Coroutine damageCoroutine;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") || other.CompareTag("Boss") && damageCoroutine == null)
        {
            // 몬스터와 충돌이 시작되면 데미지 코루틴 시작
            damageCoroutine = StartCoroutine(DealDamageOverTime(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster") || other.CompareTag("Boss") && damageCoroutine != null)
        {
            // 몬스터와의 충돌이 끝나면 데미지 코루틴 중지
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DealDamageOverTime(Collider other)
    {
        // 충돌이 유지되는 동안 반복
        while (other != null && other.gameObject.activeInHierarchy)
        {
            _player.TakeDamage(damageAmount);

            // 데미지 간격만큼 대기
            yield return new WaitForSeconds(damageInterval);
        }

        // 충돌이 종료된 경우
        damageCoroutine = null; // 코루틴이 끝날 때 참조 초기화
    }
}