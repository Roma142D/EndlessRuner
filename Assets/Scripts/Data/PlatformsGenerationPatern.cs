using System.Linq;
using UnityEngine;
using static RomanDoliba.Data.PlatformsGroupData;

namespace RomanDoliba.Data
{
    [CreateAssetMenu(fileName = "PlatformsGenerationPatern", menuName = "MyData/PlatformsGenerationPatern", order = 1)]
    public class PlatformsGenerationPatern : ScriptableObject
    {
        [SerializeField] private PlatformsGroupSpawnData[] _platformsPatterns;
        [System.NonSerialized] private Transform _parent;

        public void Init(Transform parent)
        {
            _parent = parent;
        }
        public GroupSpawnResult SpawnNextGroup(Vector3 startSpawnPosition)
        {
            var randomGroup = GetRandomGroup();
            return randomGroup.SpawnGroup(_parent, startSpawnPosition);
        }

        public PlatformsGroupData GetRandomGroup()
        {
            var chanceSum = _platformsPatterns.Sum(pattern => pattern.Chance);
            var randomValue = Random.Range(0, chanceSum);
            var currentChance = 0;

            for (int i = 0; i < _platformsPatterns.Length; i++)
            {
                currentChance += _platformsPatterns[i].Chance;

                if (randomValue < currentChance)
                {
                    return _platformsPatterns[i].Group;
                }
            }
            return _platformsPatterns.Last().Group;
        }

        [System.Serializable]
        public struct PlatformsGroupSpawnData
        {
            public PlatformsGroupData Group;
            public int Chance;
        }
    }
}
