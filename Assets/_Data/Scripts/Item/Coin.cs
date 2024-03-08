using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class Coin : MonoBehaviour
    {
        //private void Update()
        //{
        //    transform.Rotate(0, 180, 0);
        //}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                GameManager.instance.coins++;
            }
        }
    }
}
