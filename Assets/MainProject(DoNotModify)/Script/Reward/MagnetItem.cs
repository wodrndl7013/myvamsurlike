// MagnetItem.cs
using System.Collections;
using UnityEngine;

public class MagnetItem : ItemBase
{
    protected override void ApplyEffect(GameObject player)
    {
        StartCoroutine(ActivateMagnetEffect(player.transform));
    }

    private IEnumerator ActivateMagnetEffect(Transform playerTransform)
    {
        GameObject[] experienceOrbs = GameObject.FindGameObjectsWithTag("Exp");

        foreach (GameObject orb in experienceOrbs)
        {
            if (orb != null)
            {
                // 경험치 오브의 이동 기능을 직접 호출
                ExperienceOrb experienceOrb = orb.GetComponent<ExperienceOrb>();
                if (experienceOrb != null)
                {
                    experienceOrb.ActivateMagnetEffect(playerTransform);
                }
            }
        }

        yield return null; // 한 프레임 대기
    }
}