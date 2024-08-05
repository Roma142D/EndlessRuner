using RomanDoliba.Data;
using TMPro;
using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public class SpendCoins : ActionBase
    {
        [SerializeField] private int _valueToSpend;
        [SerializeField] private TextMeshProUGUI _textToChange;
        private int _curentCoinsValue;
        public override void Execute()
        {
            _curentCoinsValue = TreasuresData.GetCurrentCoinsValue();
            if (_valueToSpend <= _curentCoinsValue && _valueToSpend > 0)
            {
                TreasuresData.SpendCurrentCoinsValue(_valueToSpend);
                _curentCoinsValue -= _valueToSpend;
                _textToChange.SetText($"Coins: {_curentCoinsValue}");
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Not enough gold");
            }
        }
    }
}
