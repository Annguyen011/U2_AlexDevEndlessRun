using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class LevelGenderater : MonoBehaviour
    {
        [Header("# Level infos")]
        [SerializeField] private Transform[] levelPart;
        [SerializeField] private Transform respanPos;


        private void Update()
        {
            Transform part = levelPart[Random.Range(0, levelPart.Length)];

            Transform nePart = Instantiate(part, respanPos.position, respanPos.rotation, transform);


        }
    }
}
