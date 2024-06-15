using TMPro;
using UnityEngine;

namespace RomanDoliba.UI
{
    public class GameScreenControler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreCounterText;
        private int _curentScore;
        private float _changeableScore;
        private int _scoreMultiplier = 1;

        private void Start()
        {
            _curentScore = PlayerPrefs.GetInt("LastScore", 0);
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

        //public int CurrentScore{get{return _curentScore;}}

    }
}
