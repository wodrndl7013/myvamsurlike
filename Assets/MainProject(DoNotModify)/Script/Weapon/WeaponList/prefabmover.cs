using UnityEngine;

public class PrefabMover : MonoBehaviour
{
    // 프리팹의 이동 속도
    public float speed = 5f;

    // 프리팹이 사라지기까지의 시간 (초)
    public float destroyAfter = 5f;

    private float timer = 0f;

    void Start()
    {
        Debug.Log($"{gameObject.name} 이동 시작. 속도: {speed}, 삭제 시간: {destroyAfter}초");
    }

    void Update()
    {
        // 프리팹의 forward 방향으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // 타이머 업데이트
        timer += Time.deltaTime;

        // 일정 시간이 지나면 프리팹 삭제
        if (timer >= destroyAfter)
        {
            Debug.Log($"{gameObject.name} 삭제 (타이머: {timer}초)");
            Destroy(gameObject);
        }
    }
}