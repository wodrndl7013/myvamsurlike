using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider sfxSlider; // 효과음 슬라이더
    public Slider bgmSlider; // 배경음악 슬라이더
    public GameObject settingsPanel; // 환경설정 패널

    private void Start()
    {
        // SoundManager와 슬라이더 연결
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.BindSliders(sfxSlider, bgmSlider);
        }
        else
        {
            Debug.LogWarning("SoundManager.Instance가 초기화되지 않았습니다!");
        }
    }

    // 환경설정 패널 닫기
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
}