using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Inventory : Mark_Status {

        public override void Update()
        {
            /*if (Source && Source.Anim)
            {
                foreach (string s in GKB().Keys)
                {
                    string a = KeyBase.Translate(s);
                    if (GetKey(a) > 0 && Source.Anim)
                        Source.Anim.SetBool(a + "_Active", true);
                }
            }*/
            base.Update();
        }
    }
}