using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public class SetUnsetActiveAction : ActionBase
    {
        [SerializeField] private GameObject[] _objectsToSetActive;

        public override void Execute()
        {
            foreach (var objectToSetActive in _objectsToSetActive)
            {
                if (!objectToSetActive.activeSelf)
                {
                    objectToSetActive.SetActive(true);
                }
                else
                {
                    objectToSetActive.SetActive(false);
                }
            }
            
        }
    }
}
