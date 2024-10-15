using UnityEngine;
using UnityEngine.Serialization;

public class RotateWeapon : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public GameObject RotatePrefab;
    public int PrefabCount = 3;
    public float orbitRadius = 2f;
    public float orbitSpeed = 2f;
    public float pulsateSpeed = 1f;
    public float pulsateAmount = 0.5f;

    private GameObject[] RotateWeaponPrefabs;
    private float[] angleOffsets;

    private void Start()
    {
        InitializeRotate();
    }

    //UpdateRotatePositions은 매 프레임마다 무기들의 위치 갱신하는 함수입니다.
    private void Update()
    {
        UpdateRotatePositions();
    }

    //InitializeRotate(), PrefabCount만큼 프리팹 생성
    private void InitializeRotate()
    {
        RotateWeaponPrefabs = new GameObject[PrefabCount];
        angleOffsets = new float[PrefabCount];
        //angle에서 i의 갯수에 따라 각도를 정하고 그위치를 스폰값으로 받습니다.
        for (int i = 0; i < PrefabCount; i++)
        {
            float angle = i * (360f / PrefabCount);
            Vector3 spawnPosition = CalculateWeaponPosition(angle, orbitRadius);
            RotateWeaponPrefabs[i] = Instantiate(RotatePrefab, spawnPosition, Quaternion.identity);
            angleOffsets[i] = angle;
        }
    }
    
    //매 프레임마다 무기들의 위치 갱신하는 함수
    private void UpdateRotatePositions()
    {
        for (int i = 0; i < PrefabCount; i++)
        {
            if (RotateWeaponPrefabs[i] != null)
            {
                float currentAngle = angleOffsets[i] + (Time.time * orbitSpeed * 360f / (2f * Mathf.PI));
                float currentRadius = orbitRadius + Mathf.Sin(Time.time * pulsateSpeed) * pulsateAmount;
                Vector3 newPosition = CalculateWeaponPosition(currentAngle, currentRadius);
                RotateWeaponPrefabs[i].transform.position = newPosition;
                
                if (i > 0)
                {
                    Vector3 lookDirection = newPosition - RotateWeaponPrefabs[i].transform.position;
                    if (lookDirection != Vector3.zero)
                    {
                        RotateWeaponPrefabs[i].transform.rotation = Quaternion.LookRotation(lookDirection);
                    }
                }
            }
        }
    }

    //playerTransform.position의 이동에따른 무기의 위치를 다시 계산하는 함수
    private Vector3 CalculateWeaponPosition(float angle, float radius)
    {
        Vector3 offset = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
            0f,
            Mathf.Sin(angle * Mathf.Deg2Rad) * radius
        );
        return playerTransform.position + offset;
    }
}