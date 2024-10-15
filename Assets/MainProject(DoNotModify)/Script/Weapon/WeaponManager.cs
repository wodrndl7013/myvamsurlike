using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // 무기 ID와 Weapon 인스턴스를 매핑하는 딕셔너리
    private Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();

    // 무기 데이터 리스트 (인스펙터에서 할당)
    public List<WeaponData> weaponDataList;
    
    public GameObject rotateWeaponPrefab;
    
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