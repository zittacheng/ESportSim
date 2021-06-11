using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CCDChange : Signal {
        public string SkillKey;

        public override void InputMark(Mark M)
        {
            if (M.GetID() == SkillKey)
                M.ChangeKey("CCD", GetKey("CCDChange"));
            base.InputMark(M);
        }

        public override void CommonKeys()
        {
            // "CCDChange": CCD value change
            base.CommonKeys();
        }
    }
}