using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Shield : Mark_Status {
        public List<GameObject> BreakSignals;

        public virtual float ProcessLifeChange(float LifeChange)
        {
            if (LifeChange >= 0)
                return LifeChange;

            if (GetKey("Shield") > -LifeChange)
            {
                ChangeKey("Shield", LifeChange);
                return 0;
            }

            Break();
            return LifeChange + GetKey("Shield");
        }

        public virtual void Break()
        {
            for (int i = 0; i < BreakSignals.Count; i++)
                Source.SendSignal(BreakSignals[i], new List<string>(), Source);
            Source.RemoveStatus(this);
        }

        public override void Stack(Mark_Status M)
        {
            if (M.GetKey("Shield") > GetKey("Shield"))
                SetKey("Shield", M.GetKey("Shield"));
        }

        public override void CommonKeys()
        {
            // "Shield": Remaining shield amount
            base.CommonKeys();
        }
    }
}