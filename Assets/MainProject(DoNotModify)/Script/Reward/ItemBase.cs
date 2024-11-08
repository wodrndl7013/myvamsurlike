using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    protected abstract void ApplyEffect(GameObject player);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 아이템과 충돌했습니다.");
            ApplyEffect(other.gameObject); // 플레이어에게 효과 적용
            Destroy(gameObject); // 아이템 사용 후 제거
        }
    }
}