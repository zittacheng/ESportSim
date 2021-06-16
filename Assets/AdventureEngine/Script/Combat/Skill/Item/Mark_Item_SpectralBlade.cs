using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Item_SpectralBlade : Mark_Skill {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "AttackSpeed")
            {
                float a = Source.GetLife() / Source.GetMaxLife();
                if (a > 0.5f)
                    return Value;
                if (a < 0)
                    a = 0;
                int Stack = (int)((0.5f - a) / 0.05f);
                if ((0.5f - a) % 0.05f > 0)
                    Stack++;
                return Value * (1 + GetKey("BaseAttackSpeedMod") + GetKey("AttackSpeedMod") * Stack);
            }
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "BaseAttackSpeedMod": Base attack speed mod when triggered
            // "AttackSpeedMod": Attack speed mod change per 5% life lost
            base.CommonKeys();
        }
    }
}