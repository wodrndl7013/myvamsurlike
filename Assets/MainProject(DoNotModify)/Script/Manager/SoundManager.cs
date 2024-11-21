using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; } // 싱글톤 인스턴스

    [Header("Sound Data")]
    public List<SoundData> soundDataList; // SoundData를 직접 Inspector에 추가

    private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();
    public AudioSource bgmSource; // 배경음악 AudioSource
    public AudioSource sfxSource; // 효과음 AudioSource

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // SoundData 초기화
        InitializeSoundData();

        // 초기 볼륨 로드
        LoadVolumeSettings();
    }

    private void InitializeSoundData()
    {
        foreach (var soundData in soundDataList)
        {
            if (!audioDictionary.ContainsKey(soundData.key))
            {
                audioDictionary.Add(soundData.key, soundData.audioClip);
            }
        }
    }

    public void PlaySound(string key)
    {
        if (audioDictionary.ContainsKey(key))
        {
            sfxSource.PlayOneShot(audioDictionary[key]);
        }
        else
        {
            Debug.LogWarning($"Sound with key '{key}' not found.");
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource != null && clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
            SaveVolumeSettings();
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
            SaveVolumeSettings();
        }
    }

    public void BindSliders(Slider sfxSlider, Slider bgmSlider)
    {
        if (sfxSlider != null)
        {
            // 슬라이더 초기값 설정
            sfxSlider.value = sfxSource.volume;

            // 슬라이더 값 변경 시 호출될 이벤트 연결
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        if (bgmSlider != null)
        {
            // 슬라이더 초기값 설정
            bgmSlider.value = bgmSource.volume;

            // 슬라이더 값 변경 시 호출될 이벤트 연결
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSource.volume);
        PlayerPrefs.SetFloat("BGMVolume", bgmSource.volume);
        PlayerPrefs.Save();
    }

    public void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        }
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            SetBGMVolume(PlayerPrefs.GetFloat("BGMVolume"));
        }
    }
}
