using UnityEngine;
using System.Collections;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string WeaponID { get; private set; }
    public string WeaponName { get; private set; }
    public WeaponType WeaponType { get; private set; }
    public int CurrentLevel { get; private set; }
    private WeaponData weaponData;

    public void SetWeaponData(WeaponData data)
    {
        weaponData = data;
        WeaponID = data.weaponID;
        WeaponName = data.weaponName;
        WeaponType = data.weaponType;
        CurrentLevel = 1; // 초기 레벨 설정
    }

    public virtual void UpgradeWeapon()
    {
        if (CurrentLevel < weaponData.attackDamage.Length)
        {
            CurrentLevel++;
            Debug.Log($"Weapon {WeaponName} upgraded to level {CurrentLevel}. Attack Power: {GetCurrentAttackPower()}");
        }
        else
        {
            Debug.Log($"Weapon {WeaponName} is already at max level.");
        }
    }

    public int GetCurrentAttackPower()
    {
        return weaponData.attackDamage[CurrentLevel - 1];
    }
}

