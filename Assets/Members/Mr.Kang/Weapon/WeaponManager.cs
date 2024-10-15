using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // 무기 ID와 Weapon 인스턴스를 매핑하는 딕셔너리
    private Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();

    // 무기 데이터 리스트 (인스펙터에서 할당)
    public List<WeaponData> weaponDataList;
    
    public GameObject rotateWeaponPrefab;

    public void AddOrUpgrade(string weaponID)
    {
        if (weapons.ContainsKey(weaponID))
        {
            weapons[weaponID].UpgradeWeapon();
        }
        else
        {
            WeaponData data = GetWeaponDataByID(weaponID);
            if (data != null)
            {
                if (weaponID == "Rotate")
                {
                    GameObject rotateWeaponObj = Instantiate(rotateWeaponPrefab, transform.position, Quaternion.identity);
                    RotateWeapon rotateWeapon = rotateWeaponObj.GetComponent<RotateWeapon>();
                    if (rotateWeapon != null)
                    {
                        rotateWeapon.Initialize(data);
                        weapons.Add(weaponID, rotateWeapon);
                    }
                    else
                    {
                        Debug.LogError("RotateWeapon component not found on prefab.");
                    }
                }
                else
                {
                    // 다른 무기 타입에 대한 처리
                    Weapon newWeapon = new Weapon();
                    newWeapon.SetWeaponData(data);
                    weapons.Add(weaponID, newWeapon);
                }
            }
            else
            {
                Debug.LogWarning($"WeaponData with ID {weaponID} not found.");
            }
        }
    }
    
    private WeaponData GetWeaponDataByID(string weaponID)
    {
        foreach (var data in weaponDataList)
        {
            if (data.weaponID.Equals(weaponID, System.StringComparison.OrdinalIgnoreCase))
                return data;
        }
        return null;
    }
}