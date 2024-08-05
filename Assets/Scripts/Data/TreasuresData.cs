using UnityEngine;

namespace RomanDoliba.Data
{
    [CreateAssetMenu(fileName = "TreasureData", menuName = "MyData/TreasureData", order = 2)]
    public class TreasuresData : ScriptableObject
    {
        [SerializeField] private Currency[] _treasures;
        
        public static int GetCurrentCoinsValue()
        {
            return PlayerPrefs.GetInt("CoinsCount", 0);
        }
        public static void SpendCurrentCoinsValue(int value)
        {
            var curentCoinsValue = PlayerPrefs.GetInt("CoinsCount");
            curentCoinsValue -= value;
            PlayerPrefs.SetInt("CoinsCount", curentCoinsValue);
            PlayerPrefs.Save();
        } 

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
