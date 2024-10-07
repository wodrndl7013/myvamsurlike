using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance; // 싱글톤 패턴

    public int currentExperience = 0; // 현재 경험치

    public int level = 1; // 현재 레벨

    public int experienceTonextLevel = 100; // 다음 레벨까지 필요한 경험치
    
    public Slider experienceBar; // 경험치 바 슬라이더
    public TextMeshProUGUI levelText; // 레벨 텍스트

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    private void Start()
    {
        // 경험치 바와 레벨 텍스트 초기화
        UpdateExperienceBar();
        UpdateLevelText();
    }

    // 경험치 추가 함수
    public void AddExperience(int amount)
    {
        currentExperience += amount;
        Debug.Log("경험치 추가: " + amount);

        CheckLevelUp();
        UpdateExperienceBar(); // 경험치 바 업데이트
    }
    
    // 경험치 바 업데이트 함수
    private void UpdateExperienceBar()
    {
        if (experienceBar != null)
        {
            experienceBar.value = (float)currentExperience / experienceTonextLevel;
        }
    }
    
    // 레벨 텍스트 업데이트 함수
    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + level;
        }
    }

    // 레벨업 체크 함수
    void CheckLevelUp()
    {
        if (currentExperience >= experienceTonextLevel)
        {
            LevelUp();
        }
    }

    // 레벨업 처리
    void LevelUp()
    {
        level++;
        currentExperience -= experienceTonextLevel;
        experienceTonextLevel += 50; // 레벨이 오를 떄마다 더 많은 경험치 필요

        Debug.Log("레벨업! 현재 레벨: " + level);
        
        UpdateExperienceBar(); // 레벨업 후 경험치 바 업데이트
        UpdateLevelText(); // 레벨업 후 레벨 텍스트 업데이트
    }
}
