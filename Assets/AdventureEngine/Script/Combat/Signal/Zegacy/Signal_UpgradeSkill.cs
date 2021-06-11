using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_UpgradeSkill : Signal {
        public GameObject Ori;
        public GameObject New;

        public override void EndEffect()
        {
            if (!Target || !Target.HasSkill(Ori.GetComponent<Mark_Skill>(), out int Index))
                return;
            Target.AddSkill(New.GetComponent<Mark_Skill>(), Index);
            base.EndEffect();
        }
    }
}