using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class WeaponManager
{
    private List<IWeapon> _weapons;

    public WeaponManager()
    {
        _weapons = new List<IWeapon>();
    }

    public void AddWeapon(IWeapon weapon)
    {
        _weapons.Add(weapon);
    }

    public void UpdateWeapons()
    {
        foreach (var weapon in _weapons)
        {
            weapon.Update();
        }
    }
}
