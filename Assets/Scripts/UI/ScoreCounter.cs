using System;
using TMPro;
using UnityEngine;

namespace RomanDoliba.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreCounterText;
        [SerializeField] private TextMeshProUGUI _scoreMultiplierText;
        private int _curentScore = 0;
        private float _changeableScore;
        private int _scoreMultiplier = 1;

        private void Awake()
        {
            _scoreMultiplierText.SetText($"X" + _scoreMultiplier.ToString());
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

            if (_curentScore % 1000 == 0)
            {
                _scoreMultiplier += 1;
                _scoreMultiplierText.SetText("X" + _scoreMultiplier.ToString());
            }
        }

    }
}
