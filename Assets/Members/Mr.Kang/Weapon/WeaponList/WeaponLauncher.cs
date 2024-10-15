using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class WeaponLauncher : MonoBehaviour
{
    // 무기로 사용할 프리팹을 인스펙터에서 할당
    public GameObject weaponPrefab;

    // 감지 중심으로 사용할 Transform (예: 플레이어)
    public Transform playerTransform;

    // 적을 감지할 반경
    public float detectionRadius = 10f;

    // 무기 발사 주기 (초)
    public float launchInterval = 3f;

    // 무기 발사 속도
    public float launchSpeed = 20f;

    // 무기가 사라지기까지의 시간 (초)
    public float weaponLifetime = 5f;

    void Start()
    {
        if (weaponPrefab == null)
        {
            Debug.LogError("WeaponLauncher: 무기 프리팹이 할당되지 않았습니다!");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("WeaponLauncher: Detection Center (플레이어 Transform)이 할당되지 않았습니다!");
            return;
        }

        Debug.Log($"WeaponLauncher 시작. Detection Center 위치: {playerTransform.position}, 감지 반경: {detectionRadius}");

        // 코루틴 시작
        StartCoroutine(LaunchWeaponRoutine());
    }

    IEnumerator LaunchWeaponRoutine()
    {
        Debug.Log("LaunchWeaponRoutine 코루틴 시작");
        while (true)
        {
            // 무기 발사 주기 대기
            yield return new WaitForSeconds(launchInterval);

            // 감지된 콜라이더 확인
            Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, detectionRadius);
            Debug.Log($"OverlapSphere 호출됨. 감지된 콜라이더 수: {hitColliders.Length}");

            if (hitColliders.Length > 0)
            {
                // "Monster" 태그를 가진 콜라이더 필터링
                Collider[] monsters = System.Array.FindAll(hitColliders, collider => collider.CompareTag("Monster"));
                Debug.Log($"'Monster' 태그를 가진 몬스터 수: {monsters.Length}");

                if (monsters.Length > 0)
                {
                    foreach (Collider monster in monsters)
                    {
                        Debug.Log($"발견된 몬스터: {monster.gameObject.name}, 위치: {monster.transform.position}");
                        LaunchWeapon(monster.transform.position);
                    }
                }
                else
                {
                    Debug.Log("OverlapSphere 내에 'Monster' 태그를 가진 적이 없습니다.");
                }
            }
            else
            {
                Debug.Log("OverlapSphere 내에 아무 콜라이더도 감지되지 않았습니다.");
            }
        }
    }

    void LaunchWeapon(Vector3 targetPosition)
    {
        // 무기 생성 위치 설정 (Detection Center 위치)
        Vector3 spawnPosition = playerTransform.position;

        // 무기 생성 회전 설정 (타겟 방향으로)
        Vector3 direction = (targetPosition - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(direction);

        // 무기 인스턴스화
        GameObject weapon = Instantiate(weaponPrefab, spawnPosition, spawnRotation);
        Debug.Log($"Weapon launched towards {targetPosition} from {spawnPosition}");

        // 무기와 몬스터 간의 거리 계산
        float distance = Vector3.Distance(spawnPosition, targetPosition);
        Debug.Log($"무기와 몬스터 간의 거리: {distance}");

        // 타겟 방향으로 선 그리기 (디버그 용도)
        Debug.DrawLine(spawnPosition, targetPosition, Color.green, 2f);

        // 무기의 Rigidbody 컴포넌트 확인 및 속도 설정
        Rigidbody rb = weapon.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * launchSpeed;
            Debug.Log($"Weapon Rigidbody 속도 설정: {rb.velocity}");
        }
        else
        {
            Debug.LogWarning("WeaponPrefab에 Rigidbody가 없습니다. 이동 스크립트를 추가하세요.");
        }

        // 일정 시간 후 무기 삭제
        Destroy(weapon, weaponLifetime);
    }

    // 감지 반경 시각화를 위해 Gizmos 사용
    void OnDrawGizmosSelected()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, detectionRadius);
        }
        else
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
