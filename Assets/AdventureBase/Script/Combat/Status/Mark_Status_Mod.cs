using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Mod : Mark_Status {
        public List<string> RequiredKeys;
        public List<string> AvoidedKeys;
        public List<GameObject> SourceConditions;

        public virtual bool Trigger(Signal S)
        {
            bool T = true;
            foreach (string s in RequiredKeys)
                if (!S.HasKey(s))
                    T = false;
            foreach (string s in AvoidedKeys)
                if (S.HasKey(s))
                    T = false;
            foreach (GameObject G in SourceConditions)
            {
                if (!G.GetComponent<Condition>().Pass(Source))
                    T = false;
            }

            if (GetKey("ActiveRender") == 1)
            {
                if (T && GetKey("Render") == 0)
                {
                    SetKey("Render", 1);
                    Source.UpdateRenderStatus();
                }
                else if (!T && GetKey("Render") == 1)
                {
                    SetKey("Render", 0);
                    Source.UpdateRenderStatus();
                }
            }

            return T;
        }

        public override void CommonKeys()
        {
            // "ActiveRender": Whether to only render the status when active
            base.CommonKeys();
        }
    }
}