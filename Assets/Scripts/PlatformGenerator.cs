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
        [SerializeField] private int _platformsToSpawn;
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

            if (IsCanRelocatePlatform())
            {
                var platformToMove = _platformsSpawned[0];
                _platformsSpawned.RemoveAt(0);
                _lastPlatformSpawnedOnPosition = _platformsSpawned[_platformsSpawned.Count - 1]._platformEndPoint.position;
                platformToMove.transform.position = _lastPlatformSpawnedOnPosition - platformToMove._platformStartPoint.localPosition;
                platformToMove.ChangeObstaclePosition();
                platformToMove.SpawnTreasures();
                _platformsSpawned.Add(platformToMove);
            }
        }
        private void SpawnGroup()
        {
            while (_platformsSpawned.Count <= _platformsToSpawn)
            {
                var result = _spawnPatern.SpawnNextGroup(_lastPlatformSpawnedOnPosition);
                _platformsSpawned.AddRange(result.SpawnedPlatforms);   
                _lastPlatformSpawnedOnPosition = _platformsSpawned[_platformsSpawned.Count - 1]._platformEndPoint.position;
            }
        }
        
        private void MovePlatforms()
        {
            transform.Translate(-_platformsSpawned[0].transform.forward * Time.deltaTime * (_platformsSpeed + _platformAcceleration/100), Space.World);
            _platformAcceleration += Time.deltaTime * _platformsSpeed;
        }
        private bool IsCanRelocatePlatform()
        {
            return _platformsSpawned[0]._platformEndPoint.position.z < _mainCamera.transform.position.z;
        }
    }
}

