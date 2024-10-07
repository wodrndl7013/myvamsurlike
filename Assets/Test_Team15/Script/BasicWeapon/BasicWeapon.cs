using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicWeaponInfo
{
    public float AttackDistance;
    public float Cooltime;
    public float Damage;
    public float Speed;
}

public abstract class BasicWeapon : MonoBehaviour
{
    public BasicWeaponInfo Info; // Inspector 에서 옵션 설정 가능하게 함
    public PooledObjectType projectileType; // 풀에서 가져올 발사체 타입
    protected Vector3 moveDirection;  // 처음 설정된 이동 방향
    protected Vector3 startPosition;  // 시작 위치를 저장

    public abstract void SetPositionInfo(Vector3 mousePos, Vector3 spawnPos); // 스폰 위치 설정
}
