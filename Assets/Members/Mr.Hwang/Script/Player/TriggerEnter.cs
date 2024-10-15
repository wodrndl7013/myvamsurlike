using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mr.Hwang
{
    public class TriggerEnter : MonoBehaviour
    {
        public Player _player;

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Monster"))
            {
                _player.GetDamaged();
            }
        }
    }
}
