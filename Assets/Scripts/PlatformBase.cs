using UnityEngine;
using RomanDoliba.Data;
using System.Collections.Generic;
using RomanDoliba.PowerUp;

namespace RomanDoliba.Platforms
{
    public class PlatformBase : MonoBehaviour
    {
        [SerializeField] public Transform _platformStartPoint;
        [SerializeField] public Transform _platformEndPoint;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private GameObject[] _obstacles;
        private GameObject _spawnedObstacle;
        [Space]
        [SerializeField] private CoinsGroup _treasures;
        [SerializeField] private Transform[] _treasuresSpawnPoints;
        private List<Transform> _spawnedTreasures;
        [Space]
        [SerializeField] private PowerUps _powerUps;
        [SerializeField] private Transform[] _powerUpSpawnPoints;
        private Transform _spawnedPowerUp;

        private void Awake()
        {
            _spawnedTreasures = new List<Transform>();
        }

        public void SpawnObstacle()
        {
            var randomPosition = _spawnPoints[Random.Range(0,_spawnPoints.Length)];
            var randomObstacle = _obstacles[Random.Range(0,_obstacles.Length)];
            _spawnedObstacle = Instantiate(randomObstacle, randomPosition.position, Quaternion.identity, this.transform);
        }
        public void ChangeObstaclePosition()
        {
            var randomPosition = _spawnPoints[Random.Range(0,_spawnPoints.Length)];
            _spawnedObstacle.transform.position = randomPosition.position;
        }

        public void DestroyObstacle()
        {
            Destroy(_spawnedObstacle);
        }
        public void SpawnTreasures()
        {
            if (_spawnedTreasures.Count != 0)
            {
                foreach (var coin in _spawnedTreasures)
                {
                    if (!coin.gameObject.activeSelf)
                    {
                        coin.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                var randomPosition = _treasuresSpawnPoints[Random.Range(0, _treasuresSpawnPoints.Length)];
                _spawnedTreasures.AddRange(_treasures.SpawnCoins(randomPosition));
            }
        }
        public void SpawnPowerUp(bool spawnOnAwake)
        {
            var randomPosition = _powerUpSpawnPoints[Random.Range(0, _powerUpSpawnPoints.Length)];
            
            if (_spawnedPowerUp == null && spawnOnAwake) 
            {
                _spawnedPowerUp = _powerUps.SpawnPowerUp(randomPosition);
            }
            else if (_spawnedPowerUp != null && !_spawnedPowerUp.gameObject.activeSelf)
            {
                _spawnedPowerUp.gameObject.SetActive(true);
                _spawnedPowerUp.position = randomPosition.position;
            }
        }

    }
}

