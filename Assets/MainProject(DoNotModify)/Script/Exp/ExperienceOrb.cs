using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public int experienceAmount = 10; // 경험치 양
    public float detectionRange = 5f; // 플레이어 감지 범위
    public float moveSpeed = 10f; // 경험치 볼이 날아가는 속도
    private Transform player; // 플레이어의 위치

    private void Start()
    {
        // 태그로 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // 플레이어와 일정 범위 내에 있으면 경험치 볼이 플레이어에게 날아감
        if (distanceToPlayer <= detectionRange)
        {
            MoveToPlayer();
        }
    }
    
    // 플레이어를 향해 경험치 볼을 이동시키는 함수
    void MoveToPlayer()
    {
        // 플레이어 방향으로 이동 
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
    
    // 플레이어와 충돌 시 경험치 획득 처리
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //경험치 매니저에 경험치 추가
            ExperienceManager.Instance.AddExperience(experienceAmount);
            
            //경험치 볼 제거
            ObjectPoolManager.Instance.ReturnToPool("ExperienceOrb", gameObject);
        }
    }
}
