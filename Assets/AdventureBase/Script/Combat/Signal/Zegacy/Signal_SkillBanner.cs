using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_SkillBanner : Signal {
        public string Content;

        public override void EndEffect()
        {
            if (!Target)
                return;
            //SkillBanner.Main.Effect(Target.GetSide(), (int)GetKey("Speed"), GetKey("Critical") >= 1, Content);
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "Speed": Banner animation speed (Default = 1)
            base.CommonKeys();
        }
    }
}