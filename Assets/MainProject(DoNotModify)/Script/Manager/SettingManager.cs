using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingManager : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider bgmSlider;
    public GameObject settingsPanel; // 환경설정 패널 오브젝트 연결


    private void Start()
    {
        // 슬라이더 초기값 설정
        sfxSlider.value = SoundManager.Instance.sfxSource.volume;
        bgmSlider.value = SoundManager.Instance.bgmSource.volume;

        // 슬라이더 값이 변경될 때 이벤트 연결
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    public void SetSFXVolume(float volume)
    {
        SoundManager.Instance.SetSFXVolume(volume);
    }

    public void SetBGMVolume(float volume)
    {
        SoundManager.Instance.SetBGMVolume(volume);
    }
    
    // 환경설정 패널을 닫는 함수
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
}
