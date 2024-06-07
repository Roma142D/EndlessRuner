using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public class SetUnsetActiveAction : ActionBase
    {
        [SerializeField] private GameObject _objectToSetActive;

        public override void Execute()
        {
            if (!_objectToSetActive.activeSelf)
            {
                _objectToSetActive.SetActive(true);
            }
            else
            {
                _objectToSetActive.SetActive(false);
            }
            
        }
    }
}
