using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;

    protected virtual void Start()
    {
        
    }

    protected void Update()
    {
        MoveProjectile();
    }

    protected abstract void MoveProjectile();

    //발사체가 몹에 닿았을때의 로직
    protected void OnTriggerEnter(Collider other)
    {
        //태그는 나중에 맞게 수정, 태그 말고 다른 방식도 가능합니다.
        if (other.CompareTag("Monster"))
        {
            //몹에 몬스터 컴포넌트 부착시켜야 작동이 됩니다!
            Monster monster = other.GetComponent<Monster>();
            //몹한테 데미지 주는 로직 만들기, 이건 몬스터 자체에 사용하는게 맞는듯
            if (monster !=null)
            {
                //monster.GetDamaged(damage);
            }
            Destroy(this.gameObject);
        }
    }
}
