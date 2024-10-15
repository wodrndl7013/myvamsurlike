using UnityEngine;

[System.Serializable]
public class Ability
{
    public string abilityName; // 능력 이름
    public string description; // 능력 설명
    public Sprite icon; // 능력 아이콘
    public int power; // 능력의 효과 (예:공격력 증가, 등등)

    public void ApplyEffect(Player player)
    {
        //여기서 플레이어에게 능력을 적용하는 로직 구현 예정
    }

}
