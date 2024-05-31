using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public class SetGameOnPause : ActionBase
    {
        public override void Execute()
        {
            Time.timeScale = 0;
        }
    }
}
