using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class LedgeDetection : MonoBehaviour
    {
        #region [Abiilities]
        [Header("# LedgeDetection infos")]
        [SerializeField] private float radius;
        private bool canDetected;


        private Player player;

        #endregion

        #region [Unity Methods]
        private void Start()
        {
            player = transform.parent. GetComponent<Player>();
        }

        private void Update()
        {
            if (!canDetected) return;
            CollisionCheck();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer.Equals(player.whatisground))
            {
                canDetected = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer.Equals(player.whatisground))
            {
                canDetected = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        #endregion

        private void CollisionCheck()
        {
            player.ledgeDetected = Physics2D.OverlapCircle(transform.position,
                            radius, player.whatisground);
        }
    }
}
