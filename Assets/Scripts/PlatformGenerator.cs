using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomanDoliba.Platforms
{
    public class PlatformGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private int _platformsToPreSpawn;
        [SerializeField] private PlatformBase _platformBase;
        [SerializeField] private float _platformsSpeed;
        private List<PlatformBase> _platformsSpawned;


        private void Awake()
        {
            _platformsSpawned = new List<PlatformBase>();
            SpawnOnAwake();
            
        }
        
        void Update()
        {
            MovePlatforms();
        }

        private void SpawnOnAwake()
        {
            var spawnPosition = _startPoint.position;

            for (int i = 0; i < _platformsToPreSpawn; i++)
            {
                spawnPosition -= _platformBase._platformStartPoint.localPosition;
                var platformSpawned = Instantiate(_platformBase, spawnPosition, Quaternion.identity, this.transform);
                spawnPosition = platformSpawned._platformEndPoint.position;
                _platformsSpawned.Add(platformSpawned);
            }
        }

        private void MovePlatforms()
        {
            transform.Translate(-_platformsSpawned[0].transform.forward * _platformsSpeed * Time.deltaTime, Space.World);

            if (_platformsSpawned[0]._platformEndPoint.position.z < _startPoint.transform.position.z)
            {
                var platformToMove = _platformsSpawned[0];
                _platformsSpawned.RemoveAt(0);
                platformToMove.transform.position = _platformsSpawned[_platformsSpawned.Count -1]._platformEndPoint.position - platformToMove._platformStartPoint.localPosition;
                _platformsSpawned.Add(platformToMove);
            }
        }
    }
}

