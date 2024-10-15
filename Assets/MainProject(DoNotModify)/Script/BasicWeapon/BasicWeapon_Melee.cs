using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon_Melee : BasicWeapon
{
    private bool isMoving = false;  // 이동 중 여부 체크

    public override void SetPositionInfo(Vector3 mousePos, Vector3 spawnPos) // 무기 스폰 위치
    {
        startPosition = spawnPos;
        moveDirection = (mousePos - spawnPos).normalized;
        isMoving = true;
    }
    
    private void MoveTowardsTarget() // 무기 이동 로직
    {
        // 처음 설정된 방향으로 이동
        transform.position += moveDirection * (Info.Speed * Time.deltaTime);

        // 지정된 AttackDistance만큼 이동했으면 풀로 반환
        if (Vector3.Distance(startPosition, transform.position) >= Info.AttackDistance)
        {
            isMoving = false;
            StartCoroutine(WaitAndExecute());
        }
    }
    
    IEnumerator WaitAndExecute()// 0.5초 대기 후 풀로 리턴
    {
        yield return new WaitForSeconds(0.1f);
        ObjectPoolManager.Instance.ReturnToPool(projectileType, gameObject);
    }
    
    private void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }
    
    private void OnTriggerEnter(Collider other) // 충돌시, 충돌한 몬스터의 데미지 로직 가져옴
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            monster.GetDamaged(Info.Damage);
        }
        else if (other.CompareTag("Boss"))
        {
            Boss boss = other.GetComponent<Boss>();
            boss.GetDamaged(Info.Damage);
        }
    }
}
