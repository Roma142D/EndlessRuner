using RomanDoliba.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RomanDoliba.UI
{
    public class GameOverScreenControler : MonoBehaviour
    {
        [SerializeField] private Button _buybackButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        private int _curentCoinsValue;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonPressed);
            _curentCoinsValue = TreasuresData.GetCurrentCoinsValue();
            TryActiveBuybackButton();
        }   
        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonPressed);
        }   

        private void OnRestartButtonPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void TryActiveBuybackButton()
        {
            if (_curentCoinsValue >= 1000)
            {
                _buybackButton.interactable = true;
            }
            else
            {
                _buybackButton.interactable = false;
                var text = _buybackButton.gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
                text.SetText("Not enough \n Gold");
            }
        }   
    }
}
