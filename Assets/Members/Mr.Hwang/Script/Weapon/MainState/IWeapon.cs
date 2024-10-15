using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mr.Hwang
{
    public interface IWeapon
    {
        void Update();
        void ChangeState(WeaponState newState);
        void Attack();
    }
}

