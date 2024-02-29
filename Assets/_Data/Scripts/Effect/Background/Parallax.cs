using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class Parallax : MonoBehaviour
    {
        [Header("# Parallax infos")]
        private Vector3 cam;
        [SerializeField] private float parallaxEffect;

        private float lenght;
        private float xPosition;

        private void Start()
        {
            cam = Camera.main.transform.position;

            lenght = GetComponent<SpriteRenderer>().bounds.size.x;
            xPosition = transform.position.x; 
        }

        private void Update()
        {
            float distanceMoved = cam.x * (1 - parallaxEffect);
            float distanceToMove = cam.x  * parallaxEffect;

            transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

            if(distanceMoved > xPosition + lenght)
            {
                xPosition = xPosition + lenght;
            }

        }
    }
}
