using System.Collections;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    public Player _player;
    public float damageAmount = 10f; // 몬스터와 충돌 시 줄 데미지
    public float damageInterval = 0.5f; // 데미지 간격 (초)

    private Coroutine damageCoroutine; // 데미지 코루틴 참조 변수

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") && damageCoroutine == null)
        {
            // 몬스터와 충돌이 시작되면 데미지 코루틴 시작
            damageCoroutine = StartCoroutine(DealDamageOverTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster") && damageCoroutine != null)
        {
            // 몬스터와의 충돌이 끝나면 데미지 코루틴 중지
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (true) // 충돌이 계속되는 동안 반복
        {
            _player.TakeDamage(damageAmount); // 플레이어에게 데미지 입히기
            yield return new WaitForSeconds(damageInterval); // 데미지 간격 기다리기
        }
    }
}