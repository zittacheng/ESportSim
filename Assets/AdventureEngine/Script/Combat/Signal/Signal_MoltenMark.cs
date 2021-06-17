using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_MoltenMark : Signal {
        public GameObject MarkPrefab;
        public GameObject EffectPrefab;

        public override void EndEffect()
        {
            if (!Target)
                return;
            Target.AddStatus(MarkPrefab.GetComponent<Mark_Status>(), new List<string>());
            Mark_Status M = Target.GetStatus(MarkPrefab.GetComponent<Mark_Status>().GetID());
            if (!M)
                return;
            if (M.GetKey("Stack") >= GetKey("MaxStack"))
            {
                Target.RemoveStatus(M);
                Target.AddStatus(EffectPrefab.GetComponent<Mark_Status>(), new List<string>());
            }
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "MaxStack": Max stack to trigger effect
            base.CommonKeys();
        }
    }
}