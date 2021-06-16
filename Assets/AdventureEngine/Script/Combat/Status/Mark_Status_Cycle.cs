using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Cycle : Mark_Status {
        public List<GameObject> Signals;
        public List<string> InheritKeys;

        public override void Stack(Mark_Status M)
        {
            StackDuration(M);
        }

        public override void TimePassed(float Value)
        {
            if (!Source || !Source.CardActive())
                return;
            if (GetKey("CCD") <= 0)
            {
                List<string> AddKeys = new List<string>();
                foreach (string s in InheritKeys)
                    if (HasKey(s))
                        AddKeys.Add(KeyBase.Compose(s, GetKey(s)));
                SetKey("CCD", GetKey("CoolDown"));
                AddKeys.Add(KeyBase.Compose("TargetPositionX", Source.GetPosition().x));
                AddKeys.Add(KeyBase.Compose("TargetPositionY", Source.GetPosition().y));
                if (HasKey("ItemCount"))
                    AddKeys.Add(KeyBase.Compose("ItemCount", GetKey("ItemCount")));
                for (int i = 0; i < Signals.Count; i++)
                    Source.SendSignal(Signals[i], AddKeys, Source);
            }
            else
                ChangeKey("CCD", -Value);
            base.TimePassed(Value);
        }

        public override void CommonKeys()
        {
            // "CoolDown": Cool down for cycle effects
            // "CCD": Current cycle cool down
            base.CommonKeys();
        }
    }
}