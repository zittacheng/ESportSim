using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_ResourceMod : Mark_Status_Mod {

        public override void OutputSignal(Signal S)
        {
            if (Trigger(S))
            {
                if (S.HasKey("Mineral") && HasKey("MineralMod"))
                    S.SetKey("Mineral", S.GetKey("Mineral") * GetKey("MineralMod"));
                if (S.HasKey("Exp") && HasKey("ExpMod"))
                    S.SetKey("Exp", S.GetKey("Exp") * GetKey("ExpMod"));
            }
            base.OutputSignal(S);
        }

        public override void CommonKeys()
        {
            // "MineralMod": Mineral change modifier
            // "ExpMod": Exp change modifier
            base.CommonKeys();
        }
    }
}