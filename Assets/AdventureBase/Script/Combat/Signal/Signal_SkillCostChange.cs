using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_SkillCostChange : Signal {
        public string SkillKey;

        public override void InputMark(Mark M)
        {
            if (M.GetID() == SkillKey)
            {
                M.ChangeKey("Cost", GetKey("CostChange"));
                M.ChangeKey("CostII", GetKey("CostIIChange"));
            }
            base.InputMark(M);
        }

        public override void CommonKeys()
        {
            // "CostChange": CostI (Mineral) change
            // "CostIIChange": CostII (Exp) change
            base.CommonKeys();
        }
    }
}