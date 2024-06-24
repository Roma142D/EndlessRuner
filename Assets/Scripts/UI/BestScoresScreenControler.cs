using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace RomanDoliba.UI
{
    public class BestScoresScreenControler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _scoresTables;
        private int _lastScore;
        private int _firstBestScore;
        private int _secondBestScore;
        private int _thirdBestScore;

        private void Update()
        {
            _lastScore = PlayerPrefs.GetInt("LastScore");
            
            _firstBestScore = PlayerPrefs.GetInt("FirstBestScore");
            _secondBestScore = PlayerPrefs.GetInt("SecondBestScore");
            _thirdBestScore = PlayerPrefs.GetInt("ThirdBestScore");
            
            if (_lastScore > _firstBestScore)
            {
                _secondBestScore = _firstBestScore;
                PlayerPrefs.SetInt("SecondBestScore", _secondBestScore);
                PlayerPrefs.SetInt("FirstBestScore", _lastScore);
            }
            else if (_lastScore > _secondBestScore && _lastScore < _firstBestScore)
            {
                _thirdBestScore = _secondBestScore;
                PlayerPrefs.SetInt("ThirdBestScore", _thirdBestScore);
                PlayerPrefs.SetInt("SecondBestScore", _lastScore);
            }
            else if (_lastScore > _thirdBestScore && _lastScore < _secondBestScore)
            {
                PlayerPrefs.SetInt("ThirdBestScore", _lastScore);
            }
            PlayerPrefs.Save();

            AddScore(0, _firstBestScore);
            AddScore(1, _secondBestScore);
            AddScore(2, _thirdBestScore);
        }

        private void AddScore(int position, int score)
        {
            _scoresTables[position].SetText(score.ToString());
        }
    }
}
