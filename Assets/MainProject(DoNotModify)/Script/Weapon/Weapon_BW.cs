using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_BW : Weapon
{
    public string weapon_BW;
    void Start()
    {
        SoundManager.Instance.PlaySound(weapon_BW);
    }

    public override void LevelUpSetting()
    {
        WeaponManager.Instance._player.SettingBWValue(attackDistance, cooldown, damage, speed);
    }
}
