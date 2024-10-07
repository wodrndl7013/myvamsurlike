using UnityEngine;

public class BoneOrbitXZ : MonoBehaviour
{
    public GameObject bonePrefab;  // 회전시킬 bone prefab
    public float circleRadius = 2f;   // 회전할 반경
    public float rotationSpeed = 50f; // 회전 속도
    private GameObject boneInstance;  // 인스턴스화된 bone prefab
    private float currentDegree = 0f; // 현재 회전 각도

    void Start()
    {
        // bonePrefab 인스턴스화
        boneInstance = Instantiate(bonePrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        // 시간이 지남에 따라 회전 각도 증가
        currentDegree += Time.deltaTime * rotationSpeed;
        if (currentDegree > 360f) 
        {
            currentDegree = 0f;  // 360도를 넘으면 각도 초기화
        }

        // 각도를 라디안으로 변환
        float rad = Mathf.Deg2Rad * currentDegree;

        // x, z 좌표 계산 (xz 평면에서 회전)
        float x = circleRadius * Mathf.Cos(rad);
        float z = circleRadius * Mathf.Sin(rad);

        // boneInstance의 위치를 업데이트 (캐릭터를 기준으로 y축은 그대로 두고 회전)
        boneInstance.transform.position = transform.position + new Vector3(x, 0, z);

        // 회전 자체는 없고 무기의 방향을 유지
        boneInstance.transform.LookAt(transform.position);  // 무기가 캐릭터를 바라보게 설정
    }
}