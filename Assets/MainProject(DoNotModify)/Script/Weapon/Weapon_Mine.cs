using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Mine : Weapon
{
    void Start()
    {
        base.Start();
        StartCoroutine(SetMine());
    }

    IEnumerator SetMine()
    {
        while (true)
        {
            GameObject mine = ObjectPoolManager.Instance.SpawnFromPool(name, playerTransform.position, Quaternion.identity);
            InputValue(mine);
            yield return new WaitForSeconds(cooldown);
        }
    }
}
