using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_ChangeStack : Signal {
        public List<string> RequiredKeys;
        public List<string> AvoidedKeys;

        public override void InputMark(Mark M)
        {
            if (!Trigger(M) || !M.HasKey("Stack"))
                return;
            if (GetKey("SetMode") == 0 || !HasKey("SetMode"))
                M.ChangeKey("Stack", GetKey("Value"));
            else
                M.SetKey("Stack", GetKey("Value"));
            base.InputMark(M);
        }

        public virtual bool Trigger(Mark M)
        {
            bool T = true;
            foreach (string s in RequiredKeys)
                if (!M.HasKey(s))
                    T = false;
            foreach (string s in AvoidedKeys)
                if (M.HasKey(s))
                    T = false;
            return T;
        }

        public override void CommonKeys()
        {
            // "Value": Stack change amount or set value
            // "SetMode": Whether the value key is the target set value or not
            base.CommonKeys();
        }
    }
}