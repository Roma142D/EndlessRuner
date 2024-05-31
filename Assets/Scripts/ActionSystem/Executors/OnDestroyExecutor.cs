using RomanDoliba.ActionSystem;
using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public class OnDestroyExecutor : ExecutorBase
    {
        public void OnDestroy()
        {
            Execute();
        }        
    }
}
