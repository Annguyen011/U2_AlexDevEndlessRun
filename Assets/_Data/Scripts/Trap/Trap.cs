using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class Trap : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                Player player = collision.GetComponent<Player>();
                player.Damage();
            }
        }
    }
}
