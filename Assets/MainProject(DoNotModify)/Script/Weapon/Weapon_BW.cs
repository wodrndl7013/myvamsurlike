using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_BW : Weapon
{
    void Start()
    {
        
    }

    public override void LevelUpSetting()
    {
        WeaponManager.Instance._player.SettingBWValue(attackDistance, cooldown, damage, speed);
    }
}
