using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class CoinGenerator : MonoBehaviour
    {
        [Header("# Coin infos")]
        [SerializeField] private int amountOfCoin = 5;
        [SerializeField] private Transform coinPrefab;

        private void Start()
        {
            int addtionalOffset = amountOfCoin / 2;

            for (int i = 0; i < amountOfCoin; i++)
            {
                Vector3 offset = new Vector2(i - addtionalOffset, 0);
                Instantiate(coinPrefab, transform.position + offset, transform.rotation, transform);
            }
        }
    }
}
