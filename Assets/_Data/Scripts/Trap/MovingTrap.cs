using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class MovingTrap : MonoBehaviour
    {
        [Header("# MovingTrap infos")]
        [SerializeField] private float speed;
        [SerializeField] private float distance;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private List<Transform> movePoints = new List<Transform>();

        [SerializeField]private int indexMove;
        [SerializeField]private bool goForward;

        private void Start()
        {
            indexMove = 0;
        }

        private void Update()
        {
            if(Vector2.Distance(transform.position, movePoints[indexMove].position) <= distance)
            {
                if(indexMove == 0)
                {
                    goForward = true;
                }
                else if(indexMove >= movePoints.Count - 1) 
                {
                    goForward = false;
                }

                indexMove += (goForward) ? 1 : -1;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, movePoints[indexMove].position, 
                speed  * Time.deltaTime);
        }
    }
}
