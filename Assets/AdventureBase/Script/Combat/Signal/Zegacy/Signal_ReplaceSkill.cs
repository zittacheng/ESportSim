using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_ReplaceSkill : Signal {
        public GameObject OriSkill;
        public GameObject NewSkill;

        public override void EndEffect()
        {
            if (Target.HasSkill(OriSkill.GetComponent<Mark_Skill>(), out _))
            {
                Target.GetSkill(OriSkill.GetComponent<Mark_Skill>().GetID(), out int a);
                Target.RemoveSkill(a);
                Target.AddSkill(NewSkill.GetComponent<Mark_Skill>(), a);
            }
            base.EndEffect();
        }
    }
}