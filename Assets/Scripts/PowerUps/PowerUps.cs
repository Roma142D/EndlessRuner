using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomanDoliba.PowerUp
{
    [CreateAssetMenu(fileName = "PowerUps", menuName = "MyData/PowerUps", order = 4)]
    public class PowerUps : ScriptableObject
    {
        private List<Transform> _powerUps;
        private int _randomPowerUp;
        [Header("Shield")]
        [Space]
        [SerializeField] private Transform _shieldPrefab;
        [SerializeField] private int _imortalityDuration;
        
        public int ShieldDuration {get => _imortalityDuration;}

        private void Awake()
        {
            _powerUps = new List<Transform>();
            _powerUps.Add(_shieldPrefab);
            
        }
        public Transform SpawnPowerUp(Transform spawnPosition)
        {
            _randomPowerUp = Random.Range(0, _powerUps.Count);
            var spawnedPowerUp = Instantiate(_shieldPrefab, spawnPosition.position, Quaternion.LookRotation(Vector3.back), spawnPosition);
            
            return spawnedPowerUp;
        }
        public void ShieldToggle(Collider player, bool isActive)
        {
            if (isActive)
            {
                LayerMask obstacleLayer = LayerMask.GetMask("Obstacle");
                player.excludeLayers = obstacleLayer;
            }
            else
            {
                LayerMask nothingLayer = LayerMask.GetMask("Nothing");
                player.excludeLayers = nothingLayer;
            }
        }
        

    }
}
