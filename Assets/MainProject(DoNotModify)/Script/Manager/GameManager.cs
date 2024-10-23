using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject settingsPanel; // SettingsPanel 오브젝트를 연결
    // 게임 시작 버튼 클릭 시 호출될 메서드
    public void StartGame()
    {
        SceneManager.LoadScene("StageSelect"); // "StageSelect1" 이름의 씬을 로드
    }

    // 환경설정 버튼 클릭 시 호출될 메서드
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
        Debug.Log("환경설정 메뉴 열기");
    }

    // 나가기 버튼 클릭 시 호출될 메서드
    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit(); // 게임 종료 (에디터에서는 작동하지 않음)
    }

    // 특정 스테이지 씬으로 이동
    public void LoadStage(string stageName)
    {
        SceneManager.LoadScene("Stage1"); // 전달된 스테이지 이름으로 씬을 로드
    }
    
    // 스테이지 씬이 로드될 때 캐릭터를 스폰하는 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (CharacterManager.Instance != null && CharacterManager.Instance.selectedCharacterPrefab != null)
        {
            // 캐릭터를 스폰할 위치
            Vector3 spawnPosition = new Vector3(0, 0, 0); // 스폰할 위치 설정
            Instantiate(CharacterManager.Instance.selectedCharacterPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때 이벤트 구독
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 씬 로드 이벤트 구독 해제
    }
}