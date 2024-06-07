using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RomanDoliba.UI
{
    public class GameScreenControler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreCounterText;
        [SerializeField] private Button _pauseButton;
        private int _curentScore = 0;
        private float _changeableScore;
        private int _scoreMultiplier = 1;

        private void Start()
        {

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
            }
        }

    }
}
