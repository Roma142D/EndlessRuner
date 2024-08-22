using UnityEngine;

namespace RomanDoliba.UI
{
    public class BestScoreInMainMenu : BestScoresScreenControler
    {
        private void Awake()
        {
            AddScore(0, PlayerPrefs.GetInt("FirstBestScore"));
            AddScore(1, PlayerPrefs.GetInt("SecondBestScore"));
            AddScore(2, PlayerPrefs.GetInt("ThirdBestScore"));
        }
    }
}
