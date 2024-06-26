using UnityEngine;

namespace RomanDoliba.Data
{
    [CreateAssetMenu(fileName = "TreasureData", menuName = "MyData/TreasureData", order = 2)]
    public class TreasuresData : ScriptableObject
    {
        [SerializeField] private Currency[] _treasures;

        public int RefundCost(string name)
        {
            var cost = 0;
            foreach (var treasure in _treasures)
            {
                if (treasure.Name == name)
                {
                    cost = treasure.Cost;
                }
            }
            return cost;
        }

        [System.Serializable]
        public struct Currency
        {
            public GameObject TreasurePrefab;
            public string Name;
            public int Cost;
        }
    }
}
