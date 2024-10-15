using UnityEngine;

namespace Mr.Hwang
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
    public class WeaponData : ScriptableObject
    {
        public string weaponName;
        public float attackPower;
        public float attackSpeed;

        // 레벨에 따라 공격력이나 공격 속도 변경 가능
        public void UpdateWeaponStats(int level)
        {
            // 예시: 레벨에 따라 공격력과 공격 속도를 증가시키는 로직
            attackPower = 10f + (level * 2f);
            attackSpeed = 1f + (level * 0.1f);
        }
    }
}