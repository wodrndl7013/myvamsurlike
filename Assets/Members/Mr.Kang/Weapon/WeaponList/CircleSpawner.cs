using UnityEngine;
using System.Collections;

public class PrefabCircleSpawner : MonoBehaviour
{
    // 배치할 프리팹을 인스펙터에서 할당하세요.
    public GameObject prefab;

    // 프리팹을 배치할 중심점. 기본값은 이 스크립트가 부착된 게임 오브젝트의 위치입니다.
    public Transform center;

    // 원의 반지름
    public float radius = 6f;

    // 배치할 프리팹의 개수
    [Range(1, 360)]
    public int numberOfPrefabs = 10;

    // 프리팹이 이동할 속도
    public float speed = 5f;

    // 프리팹이 사라지기까지의 시간 (초)
    public float destroyAfter = 5f;

    // 프리팹 배치 주기 (초)
    public float spawnInterval = 3f;

    void Start()
    {
        // 중심점이 지정되지 않았다면, 이 게임 오브젝트의 위치를 사용합니다.
        if (center == null)
        {
            center = this.transform;
        }

        // 프리팹의 개수가 1 이상인지 확인
        if (numberOfPrefabs < 1)
        {
            Debug.LogWarning("numberOfPrefabs는 1 이상이어야 합니다. 기본값 1로 설정합니다.");
            numberOfPrefabs = 1;
        }

        // 프리팹이 지정되지 않았는지 확인
        if (prefab == null)
        {
            Debug.LogError("PrefabCircleSpawner: 프리팹이 할당되지 않았습니다!");
            return;
        }

        // 코루틴 시작
        StartCoroutine(SpawnPrefabsRoutine());
    }

    IEnumerator SpawnPrefabsRoutine()
    {
        Debug.Log("SpawnPrefabsRoutine 코루틴 시작");
        while (true)
        {
            Debug.Log("프리팹 스폰 시작");
            SpawnPrefabs();
            Debug.Log($"프리팹 스폰 완료. 다음 스폰까지 {spawnInterval}초 대기.");
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnPrefabs()
    {
        // 각도를 자동으로 계산 (360도를 numberOfPrefabs로 나눔)
        float angleStep = 360f / numberOfPrefabs;

        // 각도를 0도부터 360도까지 angleStep 간격으로 증가시키며 프리팹을 배치합니다.
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            float angle = i * angleStep;
            // 각도를 라디안 단위로 변환
            float rad = angle * Mathf.Deg2Rad;

            // 원의 둘레에 위치할 프리팹의 좌표 계산
            float x = center.position.x + Mathf.Cos(rad) * radius;
            float z = center.position.z + Mathf.Sin(rad) * radius;
            Vector3 position = new Vector3(x, center.position.y, z);

            // 프리팹 인스턴스화
            GameObject instance = Instantiate(prefab, position, Quaternion.identity);
            Debug.Log($"프리팹 생성: {instance.name} at {position}");

            // 프리팹이 중심에서 외부로 향하도록 회전 설정
            Vector3 direction = (position - center.position).normalized;
            if (direction != Vector3.zero)
            {
                instance.transform.rotation = Quaternion.LookRotation(direction);
            }

            // 프리팹의 부모를 설정하여 계층 구조를 정리 (선택 사항)
            instance.transform.parent = center;

            // 프리팹에 이동 스크립트 추가 및 설정
            PrefabMover mover = instance.AddComponent<PrefabMover>();
            mover.speed = speed;
            mover.destroyAfter = destroyAfter;
        }
    }
}
