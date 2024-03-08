using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("# Coin")]
        public int coins;

        private void Awake()
        {
            instance = this;
        }
    }
}
