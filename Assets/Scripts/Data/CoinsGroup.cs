using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomanDoliba.Data
{
    [CreateAssetMenu(fileName = "CoinsGroup", menuName = "MyData/CoinsGroup", order = 3)]
    public class CoinsGroup : ScriptableObject
    {
        [SerializeField] private Transform _coin;
        [SerializeField] private Transform _diamond;
        [SerializeField] private int _treasuresAmountToSpawn;

        public List<Transform> SpawnCoins(Transform spawnPoint)
        {
            var spawnedTreasures = new List<Transform>();
            var positionToSpawn = spawnPoint.position;

            for (int i = 0; i < _treasuresAmountToSpawn; i++)
            {
                if (Random.Range(0, 10) == 5)
                {
                    var treasureSpawned = Instantiate(_diamond, positionToSpawn, Quaternion.identity, spawnPoint);
                    spawnedTreasures.Add(treasureSpawned);

                    positionToSpawn.z -= 1f;
                }
                else
                {
                    var treasureSpawned = Instantiate(_coin, positionToSpawn, Quaternion.identity, spawnPoint);
                    spawnedTreasures.Add(treasureSpawned);

                    positionToSpawn.z -= 1f;
                }
            }
            return spawnedTreasures;
        }
    }
}
