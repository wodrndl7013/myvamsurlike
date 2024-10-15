using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mr.Hwang
{
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
}
