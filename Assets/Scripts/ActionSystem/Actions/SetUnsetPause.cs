using UnityEngine;

namespace RomanDoliba.ActionSystem
{
    public class SetUnsetPause : ActionBase
    {
        public override void Execute()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
