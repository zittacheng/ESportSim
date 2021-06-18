using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_SupportPower : Mark_Status {

        public override void OutputSignal(Signal S)
        {
            if (S.GetKey("AbilityPowerScaling") == 1 && S.Target && S.Source && S.Source.GetSide() == S.Target.GetSide())
            {
                if (S.HasKey("Shield"))
                    S.SetKey("Shield", S.GetKey("Shield") * GetKey("PowerMod"));
                if (S.HasKey("DamageMod"))
                {
                    float d = S.GetKey("DamageMod") - 1;
                    d *= GetKey("PowerMod");
                    S.SetKey("DamageMod", 1 + d);
                }
                if (S.HasKey("Heal"))
                    S.SetKey("Heal", S.GetKey("Heal") * GetKey("PowerMod"));
            }
            base.OutputSignal(S);
        }

        public override void CommonKeys()
        {
            // "PowerMod": Ability power scaling
            base.CommonKeys();
        }
    }
}