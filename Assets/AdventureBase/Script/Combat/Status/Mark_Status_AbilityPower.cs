using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_AbilityPower : Mark_Status {
        public List<string> RequiredKeys;

        public override void OutputSignal(Signal S)
        {
            if (S.GetKey("AbilityPowerScaling") == 1 && Trigger(S))
            {
                if (S.HasKey("Shield"))
                    S.SetKey("Shield", S.GetKey("Shield") * GetKey("PowerMod"));
                if (S.HasKey("DamageMod"))
                {
                    float d = S.GetKey("DamageMod") - 1;
                    d *= GetKey("PowerMod");
                    S.SetKey("DamageMod", 1 + d);
                }
                if (S.HasKey("SpeedMod"))
                {
                    float d = S.GetKey("SpeedMod") - 1;
                    d *= GetKey("PowerMod");
                    S.SetKey("SpeedMod", 1 + d);
                }
                if (S.HasKey("RecoveryMod"))
                {
                    float d = S.GetKey("RecoveryMod") - 1;
                    d *= GetKey("PowerMod");
                    S.SetKey("RecoveryMod", 1 + d);
                }
                if (S.HasKey("Damage"))
                    S.SetKey("Damage", S.GetKey("Damage") * GetKey("PowerMod"));
                if (S.HasKey("Heal"))
                    S.SetKey("Heal", S.GetKey("Heal") * GetKey("PowerMod"));
                if (S.HasKey("HealScale"))
                    S.SetKey("HealScale", S.GetKey("HealScale") * GetKey("PowerMod"));
            }
            base.OutputSignal(S);
        }

        public bool Trigger(Signal S)
        {
            foreach (string s in RequiredKeys)
            {
                if (S.GetKey(s) == 0)
                    return false;
            }
            return true;
        }

        public override void CommonKeys()
        {
            // "PowerMod": Ability power scaling
            base.CommonKeys();
        }
    }
}