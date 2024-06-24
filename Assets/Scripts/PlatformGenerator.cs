using System.Collections;
using System.Collections.Generic;
using RomanDoliba.Data;
using UnityEngine;

namespace RomanDoliba.Platforms
{
    public class PlatformGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private float _platformsSpeed;
        private List<PlatformBase> _platformsSpawned;
        private float _platformAcceleration = 0;
        [Space]
        [SerializeField] private PlatformsGenerationPatern _spawnPatern;
        private Vector3 _lastPlatformSpawnedOnPosition;


        private void Awake()
        {
            _platformsSpawned = new List<PlatformBase>();
            _lastPlatformSpawnedOnPosition = _startPoint.position;
            _spawnPatern.Init(transform);
            SpawnGroup();
            Time.timeScale = 1;
        }
        
        private void Update()
        {
            MovePlatforms();

            if (IsCanSpawnPlatforms())
            {
                _lastPlatformSpawnedOnPosition = _platformsSpawned[_platformsSpawned.Count - 1]._platformEndPoint.position;
                SpawnGroup();
            }
            
            for (int i = _platformsSpawned.Count-1; i >=0; i--)
            {
                var platform = _platformsSpawned[i];
                if (_platformsSpawned[i]._platformEndPoint.position.z < _mainCamera.transform.position.z)
                {
                    _platformsSpawned.Remove(platform);
                    Destroy(platform.gameObject);
                }
            }
        }
        private void SpawnGroup()
        {
            var result = _spawnPatern.SpawnNextGroup(_lastPlatformSpawnedOnPosition);
            _platformsSpawned.AddRange(result.SpawnedPlatforms);
        }
        
        private void MovePlatforms()
        {
            transform.Translate(-_platformsSpawned[0].transform.forward * Time.deltaTime * (_platformsSpeed + _platformAcceleration/100), Space.World);
            _platformAcceleration += Time.deltaTime * _platformsSpeed;
        }
        private bool IsCanSpawnPlatforms()
        {
            return _platformsSpawned[0]._platformEndPoint.position.z < _mainCamera.transform.position.z && _platformsSpawned.Count <= 15;
        }
    }
}

