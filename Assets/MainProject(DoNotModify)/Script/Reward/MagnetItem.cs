using System.Collections;
using UnityEngine;

public class MagnetItem : ItemBase
{
    protected override void ApplyEffect(GameObject player)
    {
        StartCoroutine(MoveExperienceOrbsToPlayer(player));
    }

    private IEnumerator MoveExperienceOrbsToPlayer(GameObject player)
    {
        GameObject[] experienceOrbs = GameObject.FindGameObjectsWithTag("Exp");
        float pullSpeed = 5f; // 오브들이 이동하는 속도

        while (experienceOrbs.Length > 0)
        {
            foreach (GameObject orb in experienceOrbs)
            {
                if (orb != null)
                {
                    orb.transform.position = Vector3.MoveTowards(orb.transform.position, player.transform.position, pullSpeed * Time.deltaTime);
                }
            }

            yield return null;
        }
    }
}