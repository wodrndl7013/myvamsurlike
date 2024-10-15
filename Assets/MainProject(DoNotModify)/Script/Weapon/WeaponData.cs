using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data", order = 51)]
public class WeaponData : ScriptableObject
{
    public string weaponID; // 무기 고유 ID
    public string weaponName; // 무기 이름
    public WeaponType weaponType; // 무기 타입 (예: Sword, Bow 등)
    public int[] attackDamage; // 각 레벨별 공격력
    public float[] attackSpeed;
    public float[] range; 
}
