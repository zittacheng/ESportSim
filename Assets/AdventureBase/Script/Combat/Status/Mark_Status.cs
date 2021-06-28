using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status : Mark {

        public virtual void Stack(Mark_Status M)
        {

        }

        public void StackCount(Mark_Status M)
        {
            if (!HasKey("Stack") || !M.HasKey("Stack"))
                return;
            ChangeKey("Stack", M.GetKey("Stack"));
        }

        public void StackDuration(Mark_Status M)
        {
            if (!HasKey("Duration") || !M.HasKey("Duration"))
                return;
            if (M.GetKey("Duration") > GetKey("Duration"))
                SetKey("Duration", M.GetKey("Duration"));
        }

        /*public override void EndOfTurn()
        {
            if (HasKey("Duration"))
                ChangeKey("Duration", -1);
            base.EndOfTurn();
            if (HasKey("Duration") && GetKey("Duration") <= 0)
            {
                CombatRemove();
                Source.RemoveStatus(this);
            }
        }*/

        public virtual void CombatRemove()
        {

        }

        public override void EndOfCombat()
        {
            if (!HasKey("Permanent") || GetKey("Permanent") == 0)
                Source.RemoveStatus(this);
            base.EndOfCombat();
        }

        public override void Death()
        {
            if (!HasKey("Permanent") || GetKey("Permanent") == 0)
                Source.RemoveStatus(this);
            base.Death();
        }

        public override void TimePassed(float Value)
        {
            if (HasKey("Duration"))
                ChangeKey("Duration", -Value);
            base.TimePassed(Value);
            if (HasKey("Duration") && GetKey("Duration") <= 0)
            {
                CombatRemove();
                Source.RemoveStatus(this);
            }
        }

        public override void CommonKeys()
        {
            // "Render": Whether the status should be rendered
            // "Duration": Current duration
            // "Stack": Current stack
            // "IgnoreStack": Whether the status should ignore stacking
            // "Passive": Whether the status comes from passive marks
            // "Permanent": Whether the status should not be removed after combat
            base.CommonKeys();
        }
    }
}