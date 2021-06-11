using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControl_Combo : AIControl {
        public List<ComboUnit> Combos;
        public ComboUnit CurrentCombo;
        public int CurrentIndex = -1;

        public override Mark_Skill GetSkill()
        {
            if (!CurrentCombo)
                NewCombo();
            else
                CurrentIndex++;
            Mark_Skill S = CurrentCombo.GetSkill(CurrentIndex);
            if (!S)
            {
                NewCombo();
                S = CurrentCombo.GetSkill(CurrentIndex);
            }
            if (!S || !S.CanUse())
                return base.GetSkill();
            return S;
        }

        public void NewCombo()
        {
            CurrentCombo = Combos[Random.Range(0, Combos.Count)];
            CurrentIndex = 0;
        }
    }
}