using RomanDoliba.ActionSystem;
using UnityEngine;
using UnityEngine.UI;

namespace RomanDoliba.ActionSystem
{
    public class OnUIButtonPresed : ExecutorBase
    {
        [SerializeField] private Button _button;
        void Start()
        {
            _button.onClick.AddListener(Execute);
        }

        private void Execute()
        {
            Execute(null);
        }
    }
}
