using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomanDoliba.Platforms.Coins
{
    public class CoinsSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _coinsOnPlatform;
        private int _coinsAmountToSpawn;

        private void Awake()
        {
            _coinsOnPlatform = new List<Transform>();
            _coinsAmountToSpawn = Random.Range(1, _coinsOnPlatform.Count);
            SpawnCoins(_coinsAmountToSpawn);
        }

        public void SpawnCoins(int coinsAmount)
        {
            if (_coinsOnPlatform.Count != 0 && coinsAmount <= _coinsOnPlatform.Count)
            {
                for (int i = 0; i < coinsAmount; i++)
                {
                    var coinToSpawn = _coinsOnPlatform[Random.Range(0, _coinsOnPlatform.Count - 1)];
                    if (coinToSpawn.gameObject.activeSelf)
                    {
                        i -= 1;
                    }
                    else
                    {
                        coinToSpawn.gameObject.SetActive(true);
                    }
                }
            }
            
        }
    }
}
