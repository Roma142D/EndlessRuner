using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public class DestroyObjectAction : ActionBase
    {
        [SerializeField] private GameObject _objectToDestroy;
        [SerializeField] private float _delay;

        public override void Execute()
        {
            Destroy(_objectToDestroy, _delay);
        }
    }
}
