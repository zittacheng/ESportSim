using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Ignite : Signal_Damage {
        public List<string> RequiredKeys;
        [HideInInspector] public List<Mark_Status> RemovingStatus;

        public override void InputMark(Mark M)
        {
            bool Trigger = true;
            foreach (string s in RequiredKeys)
                if (!M.HasKey(s))
                    Trigger = false;
            if (Trigger)
            {
                if (M.HasKey("Stack"))
                    ChangeKey("IgniteCount", M.GetKey("Stack"));
                else
                    ChangeKey("IgniteCount", 1);
                if (M.HasKey("Stack"))
                    ChangeKey("Damage", GetKey("IgniteDamage") * M.GetKey("Stack"));
                else
                    ChangeKey("Damage", GetKey("IgniteDamage"));
                if (GetKey("Removed") == 1 && M.GetComponent<Mark_Status>())
                    RemovingStatus.Add(M.GetComponent<Mark_Status>());
            }
            base.InputMark(M);
        }

        public override void SubEndEffect()
        {
            for (int i = RemovingStatus.Count - 1; i >= 0; i--)
                RemovingStatus[i].Source.RemoveStatus(RemovingStatus[i]);
            base.SubEndEffect();
        }

        public override void CommonKeys()
        {
            // "IgniteDamage": Damage for each status
            // "IgniteCount": Number of status ignited
            // "Removed": Whether status should be removed when ignited
            base.CommonKeys(); 
        }
    }
}