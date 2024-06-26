using RomanDoliba.ActionSystem;
using TMPro;
using UnityEngine;

namespace RomanDoliba.UI
{
    public class GameScreenControler : MonoBehaviour
    {
        [Header("Score Counter")]
        [SerializeField] private TextMeshProUGUI _scoreCounterText;
        private int _curentScore;
        private float _changeableScore;
        private int _scoreMultiplier = 1;
        [Space]
        [Header("Coins Counter")]
        [SerializeField] private TextMeshProUGUI _coinsCounterText;
        [SerializeField] private string _coinsCollectEventName;
        [SerializeField] private string _diamondsCollectEventName;
        [SerializeField] private int _coinsAmountPerEvent;
        private int _curentCoinsValue;

        private void Awake()
        {
            _curentScore = PlayerPrefs.GetInt("LastScore", 0);
            _curentCoinsValue = PlayerPrefs.GetInt("CoinsCount", 0);

            _coinsCounterText.SetText($"Coins: {_curentCoinsValue}");
            GlobalEventSender.OnEvent += OnColectEvent;
        }
        private void FixedUpdate()
        {
            _scoreCounterText.SetText(_curentScore.ToString());
            
            ChangingScore();
        }        

        public void ChangingScore()
        {
            _changeableScore += (1 * _scoreMultiplier) * 0.01f;
            _curentScore = (int)_changeableScore;

            PlayerPrefs.SetInt("LastScore", _curentScore);
            PlayerPrefs.Save();

            if (_curentScore % 1000 == 0)
            {
                _scoreMultiplier += 1;
            }
        }

        private void OnColectEvent(string eventName)
        {
            if (eventName == _coinsCollectEventName)
            {
                _curentCoinsValue += _coinsAmountPerEvent;
                PlayerPrefs.SetInt("CoinsCount", _curentCoinsValue);
                PlayerPrefs.Save();

                _coinsCounterText.SetText($"Coins: {_curentCoinsValue}");
            }
            else if (eventName == _diamondsCollectEventName)
            {
                _curentCoinsValue += _coinsAmountPerEvent * 10;
                PlayerPrefs.SetInt("CoinsCount", _curentCoinsValue);
                PlayerPrefs.Save();

                _coinsCounterText.SetText($"Coins: {_curentCoinsValue}");
            }
        }

    }
}
