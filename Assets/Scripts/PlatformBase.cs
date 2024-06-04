using UnityEngine;

namespace RomanDoliba.Platforms
{
    public class PlatformBase : MonoBehaviour
    {
        [SerializeField] public Transform _platformStartPoint;
        [SerializeField] public Transform _platformEndPoint;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private GameObject[] _obstacles;
        private GameObject _spawnedObstacle;

        public void SpawnObstacle()
        {
            var randomPosition = _spawnPoints[Random.Range(0,_spawnPoints.Length)];
            var randomObstacle = _obstacles[Random.Range(0,_obstacles.Length)];
            
            _spawnedObstacle = Instantiate(randomObstacle, randomPosition.position, Quaternion.identity, this.transform);
        }

        public void DestroyObstacle()
        {
            Destroy(_spawnedObstacle);
        }
    }
}

