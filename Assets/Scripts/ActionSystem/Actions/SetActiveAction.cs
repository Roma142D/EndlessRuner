using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public class SetActiveAction : ActionBase
    {
        [SerializeField] private GameObject _objectToSetActive;

        public override void Execute()
        {
            _objectToSetActive.SetActive(true);
        }
    }
}
