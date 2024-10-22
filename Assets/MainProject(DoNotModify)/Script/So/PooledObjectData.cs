using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPooledObject", menuName = "Pooling/PooledObject")]
public class PooledObjectData : ScriptableObject
{
    public string objectTypeName; // 오브젝트 타입을 구분하기 위한 이름
    public GameObject prefab;     // 풀링할 프리팹
    public int poolSize;          // 풀 크기

    private void Awake()
    {
        objectTypeName = this.name;
        Debug.Log($"{this.name}설정완료");
    }
}


