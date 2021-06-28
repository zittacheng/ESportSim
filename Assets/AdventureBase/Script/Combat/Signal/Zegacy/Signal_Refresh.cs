using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Refresh : Signal {

        public override void InputMark(Mark M)
        {
            if (M.HasKey("CoolDown") && !M.HasKey("UnRefreshed"))
            {
                if (!HasKey("Refresh") || M.GetKey("CCD") <= GetKey("Refresh"))
                    M.SetKey("CCD", 0);
                else
                    M.ChangeKey("CCD", -GetKey("Refresh"));
            }
            base.InputMark(M);
        }

        public override void CommonKeys()
        {
            // "UnRefreshed": Whether the skill cannot be refreshed
            // "Refresh": The amount refreshed
            base.CommonKeys();
        }
    }
}