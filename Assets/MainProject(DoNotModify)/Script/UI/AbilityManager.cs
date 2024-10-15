using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    public GameObject abilitySelectionPanel; // 능력 선택 패널
    public List<Ability> abilities; // 가능한 능력 리스트
    public Transform abilityCardContainer; // 능력 카드들을 담는 컨테이너
    public GameObject abilityCardPrefab; // 능력 카드 프리팹

    private Player player; // 플레이어 참조 (ExperienceManager에서 설정)

    private void Start()
    {
        abilitySelectionPanel.SetActive(false);
    }

    public void Initialize(Player player)
    {
        this.player = player;
    }

    public void ShowAbilitySelection()
    {
        //게임을 멈추기 위해 Time.timescale을 0으로 설정
        Time.timeScale = 0f;
        
        abilitySelectionPanel.SetActive(true);

        // 기존 카드가 있다면 삭제
        foreach (Transform child in abilityCardContainer)
        {
            Destroy(child.gameObject);
        }

        // 무작위로 능력 3개를 선택해 카드 생성
        List<Ability> randomAbilities = GetRandomAbilities(3);
        foreach (Ability ability in randomAbilities)
        {
            GameObject card = Instantiate(abilityCardPrefab, abilityCardContainer);
            // 카드 UI 설정
            card.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = ability.abilityName;
            card.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = ability.description;
            card.transform.Find("Icon").GetComponent<Image>().sprite = ability.icon;

            Button selectButton = card.transform.Find("SelectButton").GetComponent<Button>();
            selectButton.onClick.AddListener(() => SelectAbility(ability));
        }
    }

    void SelectAbility(Ability ability)
    {
        // 능력을 플레이어에게 적용
        ability.ApplyEffect(player);

        // 패널 비활성화
        abilitySelectionPanel.SetActive(false);
        
        // 게임을 다시 재개하기 위해 Time.timescale을 1로 설정
        Time.timeScale = 1f;
    }

    List<Ability> GetRandomAbilities(int count)
    {
        List<Ability> selectedAbilities = new List<Ability>();
        List<Ability> availableAbilities = new List<Ability>(abilities);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableAbilities.Count);
            selectedAbilities.Add(availableAbilities[randomIndex]);
            availableAbilities.RemoveAt(randomIndex);
        }

        return selectedAbilities;
    }
}
