using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public class ExecutorBase : MonoBehaviour
    {
        [SerializeField] private ConditionBase _condition;
        [SerializeField] private ActionBase[] _actions;

        public void Execute()
        {
            if (_condition == null || _condition.Check())
            {
                foreach (var action in _actions)
                {
                    action.Execute();
                }
            }
        }
    }
}
