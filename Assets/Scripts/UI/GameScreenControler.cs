using TMPro;
using UnityEngine;

namespace RomanDoliba.UI
{
    public class GameScreenControler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreCounterText;
        private int _curentScore = 0;
        private float _changeableScore;
        private int _scoreMultiplier = 1;

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
