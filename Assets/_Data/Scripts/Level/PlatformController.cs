using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private SpriteRenderer headerSprite;

        private void Start()
        {
            headerSprite.transform.parent = transform.parent;
            headerSprite.transform.localPosition = new Vector3(sprite.bounds.size.x, .2f);
            headerSprite.transform.position = new Vector3(transform.position.x, sprite.bounds.max.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                headerSprite.color = GameManager.instance.platfromColor;
            }
        }
    }
}
