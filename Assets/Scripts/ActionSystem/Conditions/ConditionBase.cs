using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public abstract class ConditionBase : MonoBehaviour
    {
        public abstract bool Check(object data = null);
    }
}
