using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RomanDoliba.Platforms;

namespace RomanDoliba.Data
{
    [CreateAssetMenu(fileName = "PlatformsGroupData", menuName = "MyData/PlatformsGroupData", order = 0)]
    public class PlatformsGroupData : ScriptableObject
    {
        [SerializeField] private PlatformBase[] _platforms;

        public GroupSpawnResult SpawnGroup(Transform parent, Vector3 starSpawnPosition)
        {
            var spawnedPlatforms = new List<PlatformBase>();
            var lastSpawnedPosition = new Vector3(0, 0, 0);
            var zeroPosition = new Vector3(0, 0, 0);

            for (int i = 0; i < _platforms.Length; i++)
            {
                if (lastSpawnedPosition == zeroPosition)
                {
                    lastSpawnedPosition = starSpawnPosition;
                }
                lastSpawnedPosition -= _platforms[i]._platformStartPoint.localPosition;

                var platformSpawned = Instantiate(_platforms[i], lastSpawnedPosition, Quaternion.identity, parent);
                platformSpawned.SpawnObstacle();

                lastSpawnedPosition = platformSpawned._platformEndPoint.position;

                spawnedPlatforms.Add(platformSpawned);
            }
            return new GroupSpawnResult(spawnedPlatforms, lastSpawnedPosition);
        }

        public struct GroupSpawnResult
        {
            public readonly List<PlatformBase> SpawnedPlatforms;
            public readonly Vector3 LastSpawnedPosition;
            public GroupSpawnResult(List<PlatformBase> spawnedPlatforms, Vector3 lastSpawnedPosition)
            {
                SpawnedPlatforms = spawnedPlatforms;
                LastSpawnedPosition = lastSpawnedPosition;
            }
        }
    }
    
}
