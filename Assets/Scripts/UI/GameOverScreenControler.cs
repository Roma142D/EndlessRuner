using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RomanDoliba.UI
{
    public class GameOverScreenControler : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonPressed);

        }   
        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonPressed);
        }   

        private void OnRestartButtonPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }  
    }
}
