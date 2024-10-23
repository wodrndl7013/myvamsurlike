using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject character1Prefab; // 첫 번째 캐릭터 프리팹
    public GameObject character2Prefab; // 두 번째 캐릭터 프리팹

    // 첫 번째 캐릭터 선택
    public void SelectCharacter1()
    {
        CharacterManager.Instance.SelectCharacter(character1Prefab); // 첫 번째 캐릭터 선택
    }

    // 두 번째 캐릭터 선택
    public void SelectCharacter2()
    {
        CharacterManager.Instance.SelectCharacter(character2Prefab); // 두 번째 캐릭터 선택
    }
}
