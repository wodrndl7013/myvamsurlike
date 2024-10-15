using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeBomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.GetDamaged();
        }
    }
}