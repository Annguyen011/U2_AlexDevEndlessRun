using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class LevelGenderater : MonoBehaviour
    {
        [Header("# Level infos")]
        [SerializeField] private Transform[] levelPart;
        [SerializeField] private Vector3 netPartPos;
        [SerializeField] private float distanceToSpan;
        [SerializeField] private float distanceToDelete;
        [SerializeField] private Transform player;

        private void Update()
        {
            GenderatePlatform();

            DeletePlatform();
        }

        private void GenderatePlatform()
        {
            while (Vector2.Distance(player.position, netPartPos) < distanceToSpan)
            {

                Transform part = levelPart[Random.Range(0, levelPart.Length)];

                Transform nePart = Instantiate(part, netPartPos - part.Find("StartPoint").position, transform.rotation, transform);

                netPartPos = part.Find("EndPoint").position;
            }
        }

        private void DeletePlatform()
        {
            if (transform.childCount == 0) return;

            Transform partToDelete  = transform.GetChild(0);

            if(Vector2.Distance(player.transform.position, partToDelete.position) > distanceToDelete)
            {
                Destroy(partToDelete.gameObject);
            }
        }
    }
}
