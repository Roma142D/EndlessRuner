using RomanDoliba.ActionSystem;
using UnityEngine;
using UnityEngine.UI;

namespace RomanDoliba.ActionSystem
{
    public class OnUIButtonPresed : ExecutorBase
    {
        [SerializeField] private Button _button;
        void OnEnable()
        {
            _button.onClick.AddListener(Execute);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(Execute);
        }

        private void Execute()
        {
            Execute(null);
        }
    }
}
